using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmallPortal.Data;
using SmallPortal.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

namespace SmallPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public AdminController(ApplicationDbContext context,
            IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recipient1099Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recipient1099Create([Bind("Email,Password,ConfirmPassword,BusinessName,FirstName,LastName,Phone,TaxIDNumber,StreetAddress,City,State,PostalCode,box1,box2,box3,box4,box5,box6,box7,box8,box9,box10,box12,box13,box14,box15,box16,box17")] Recipient1099InputModel recipient1099InputModel)
        {
            if (ModelState.IsValid)
            {
                // Getting payer info to submit in 1099MISC 
                var intuitPayerId = (from u in _context.Users
                         where u.UserName == User.Identity.Name
                          select u).SingleOrDefault();

                IntuitAuth auth;
                string username = "AB0y9jR3T1zZ2y4lgSTVXm8ZAz85V8NQLbWpyFtD7zn5n-test";
                string password = "jSQkNe8QNnOXwAOrUmXjU84ZL4DjJekADgZaMF4y";
                string IntuiAuthURL = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer";
                string accessToken;
                string intuitRecipientId = "";

                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                var content = new FormUrlEncodedContent(values);


                var authenticationString = $"{username}:{password}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                var request = new HttpRequestMessage(HttpMethod.Post, IntuiAuthURL);
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                request.Content = content;

                var client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    auth = await response.Content.ReadFromJsonAsync<IntuitAuth>();
                    accessToken = auth.Access_token;


                    string CreateContactURL = @"https://formfly.api.intuit.com/v1/contacts";

                    var CreateContactBody = new IntuitCreateContactBody
                    {
                        //metadata = (new Metadata { id = "payer1" }),
                        businessName = recipient1099InputModel.BusinessName,
                        firstName = recipient1099InputModel.FirstName,
                        lastName = recipient1099InputModel.LastName,
                        streetAddress = recipient1099InputModel.StreetAddress,
                        city = recipient1099InputModel.City,
                        state = recipient1099InputModel.State,
                        postalCode = recipient1099InputModel.PostalCode,
                        phone = recipient1099InputModel.Phone,
                        email = recipient1099InputModel.Email,
                        tin = recipient1099InputModel.TaxIDNumber
                    };

                    var CreateContactRequest = new HttpRequestMessage(HttpMethod.Post, CreateContactURL);

                    CreateContactRequest.Content = new StringContent(
                        JsonSerializer.Serialize(CreateContactBody),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var ContactClient = _clientFactory.CreateClient();
                    ContactClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage ContactResponse = await ContactClient.SendAsync(CreateContactRequest);
                    if (ContactResponse.IsSuccessStatusCode)
                    {
                        IntuitCreateContact newContact = await ContactResponse.Content.ReadFromJsonAsync<IntuitCreateContact>();
                        intuitRecipientId = newContact.id;
                    }

                    var New1099 = new Recipient1099
                    {
                        //actions = new SmallPortal.Models.Actions { submit = true },
                        deliveryOptions = (new DeliveryOptions { mail = true }),
                        payer = (new Payer
                        {
                            id = intuitPayerId.IntuitId,
                            firstName = intuitPayerId.FirstName,
                            lastName = intuitPayerId.LastName,
                            streetAddress = intuitPayerId.StreetAddress,
                            city = intuitPayerId.City,
                            state = intuitPayerId.State,
                            postalCode = intuitPayerId.PostalCode,
                            phone = intuitPayerId.Phone,
                            email = intuitPayerId.Email,
                            tin = intuitPayerId.TaxIDNumber,
                            validationStatus = "valid"
                        }),
                        recipient = (new Recipient
                        {
                            businessName = recipient1099InputModel.BusinessName,
                            streetAddress = recipient1099InputModel.StreetAddress,
                            city = recipient1099InputModel.City,
                            state = recipient1099InputModel.State,
                            postalCode = recipient1099InputModel.PostalCode,
                            phone = recipient1099InputModel.Phone,
                            email = recipient1099InputModel.Email,
                            tin = recipient1099InputModel.TaxIDNumber
                        }),
                        boxValues = (new Boxvalues
                        {
                            box1 = recipient1099InputModel.box1,
                            box2 = recipient1099InputModel.box2,
                            box3 = recipient1099InputModel.box3,
                            box4 = recipient1099InputModel.box4,
                            box5 = recipient1099InputModel.box5,
                            box6 = recipient1099InputModel.box6,
                            box7 = recipient1099InputModel.box7,
                            box8 = recipient1099InputModel.box8,
                            box9 = recipient1099InputModel.box9,
                            box10 = recipient1099InputModel.box10,
                            box12 = recipient1099InputModel.box12,
                            box13 = recipient1099InputModel.box13,
                            box14 = recipient1099InputModel.box14,
                            box15 = recipient1099InputModel.box15,
                            box16 = recipient1099InputModel.box16,
                            box17 = recipient1099InputModel.box17
                        })
                    };

                    string CreateRecipient1099URL = "https://formfly.api.intuit.com/v2/forms/2020/1099-misc";

                    var CreateRecipient1099 = new HttpRequestMessage(HttpMethod.Post, CreateRecipient1099URL);

                    CreateRecipient1099.Content = new StringContent(
                        JsonSerializer.Serialize(New1099),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var Recipient1099Client = _clientFactory.CreateClient();
                    Recipient1099Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage Recipient1099Response = await Recipient1099Client.SendAsync(CreateRecipient1099);
                    if (Recipient1099Response.IsSuccessStatusCode)
                    {
                        IntuitCreate1099 new1099 = await Recipient1099Response.Content.ReadFromJsonAsync<IntuitCreate1099>();
                        string formId = new1099.id;
                    } else
                    {
                        IntuitError newError = await Recipient1099Response.Content.ReadFromJsonAsync<IntuitError>();
                        Console.WriteLine(newError);
                    }
                }
                    //    _context.Add(recipient1099InputModel);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(recipient1099InputModel);
        }
    }
}