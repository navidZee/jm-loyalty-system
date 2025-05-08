using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMLS.RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the activity")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Title or name of the activity"),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Type or category of the activity"),
                    PointsReward = table.Column<int>(type: "int", nullable: false, comment: "Number of points rewarded for completing the activity"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Optional detailed description of the activity"),
                    ExpirationPeriod = table.Column<TimeSpan>(type: "time", nullable: true, comment: "Optional time span after which the earned points from this activity expire"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp when the offer was created"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp when the offer was last updated")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the offer")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false, comment: "Unique code representing this offer"),
                    PointsCost = table.Column<int>(type: "int", nullable: false, comment: "Points required by the customer to redeem this offer"),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Classification or type of the offer"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Value or discount amount associated with the offer"),
                    ReferenceId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the entity this offer is linked to (e.g., product ID)"),
                    ReferenceType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Type of the entity referenced by this offer (e.g., product, category)"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp when the offer was created"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Timestamp when the offer was last updated")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the points record")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the customer who owns this points record"),
                    Balance = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Total available points for the customer"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when this points record was created"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when this points record was last updated")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointsEarned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for this earned points record")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the entity this earn is linked to (e.g., purchasesId, SocialMedia activity , ...)"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Expiration date of the earned points, if applicable"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when this earned points record was created"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when this earned points record was last updated"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    PointsReward = table.Column<int>(type: "int", nullable: false, comment: "Number of points earned from the associated activity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsEarned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsEarned_Activity",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointsEarned_Point",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointsSpent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the point account")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the point account was created"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the point account was last modified"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    PointsCost = table.Column<int>(type: "int", nullable: false, comment: "Current point balance for the customer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsSpent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsSpent_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointsSpent_Point",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Points_CustomerId",
                table: "Points",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointsEarned_ActivityId",
                table: "PointsEarned",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsEarned_PointId",
                table: "PointsEarned",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsSpent_OfferId",
                table: "PointsSpent",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsSpent_PointId",
                table: "PointsSpent",
                column: "PointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointsEarned");

            migrationBuilder.DropTable(
                name: "PointsSpent");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
