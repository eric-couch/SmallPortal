using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallPortal.Data.Migrations
{
    public partial class added1099 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxvalues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    box1 = table.Column<string>(nullable: true),
                    box2 = table.Column<string>(nullable: true),
                    box3 = table.Column<string>(nullable: true),
                    box4 = table.Column<string>(nullable: true),
                    box5 = table.Column<string>(nullable: true),
                    box6 = table.Column<string>(nullable: true),
                    box7 = table.Column<bool>(nullable: false),
                    box8 = table.Column<string>(nullable: true),
                    box9 = table.Column<string>(nullable: true),
                    box10 = table.Column<string>(nullable: true),
                    box12 = table.Column<string>(nullable: true),
                    box13 = table.Column<string>(nullable: true),
                    box14 = table.Column<string>(nullable: true),
                    box15 = table.Column<string>(nullable: true),
                    box16 = table.Column<string>(nullable: true),
                    box17 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxvalues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payer",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    businessName = table.Column<string>(nullable: true),
                    streetAddress = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    tin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipient1099",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deliveryOptionsId = table.Column<int>(nullable: true),
                    payerid = table.Column<string>(nullable: true),
                    recipientId = table.Column<int>(nullable: true),
                    boxValuesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient1099", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipient1099_Boxvalues_boxValuesId",
                        column: x => x.boxValuesId,
                        principalTable: "Boxvalues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipient1099_DeliveryOptions_deliveryOptionsId",
                        column: x => x.deliveryOptionsId,
                        principalTable: "DeliveryOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipient1099_Payer_payerid",
                        column: x => x.payerid,
                        principalTable: "Payer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipient1099_Recipient_recipientId",
                        column: x => x.recipientId,
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipient1099_boxValuesId",
                table: "Recipient1099",
                column: "boxValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient1099_deliveryOptionsId",
                table: "Recipient1099",
                column: "deliveryOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient1099_payerid",
                table: "Recipient1099",
                column: "payerid");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient1099_recipientId",
                table: "Recipient1099",
                column: "recipientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipient1099");

            migrationBuilder.DropTable(
                name: "Boxvalues");

            migrationBuilder.DropTable(
                name: "DeliveryOptions");

            migrationBuilder.DropTable(
                name: "Payer");

            migrationBuilder.DropTable(
                name: "Recipient");
        }
    }
}
