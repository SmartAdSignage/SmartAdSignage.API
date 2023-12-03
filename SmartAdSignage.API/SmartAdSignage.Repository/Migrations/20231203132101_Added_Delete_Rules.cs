using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAdSignage.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Added_Delete_Rules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdCampaigns_AspNetUsers_UserId",
                table: "AdCampaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAdvertisements_AdCampaigns_AdCampaignId",
                table: "CampaignAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAdvertisements_Advertisements_AdvertisementId",
                table: "CampaignAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_IoTDevices_Panels_PanelId",
                table: "IoTDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_AspNetUsers_UserId",
                table: "Panels");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_Locations_LocationId",
                table: "Panels");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Advertisements_AdvertisementId",
                table: "Queues");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Panels_PanelId",
                table: "Queues");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "CampaignAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdCampaignId",
                table: "CampaignAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdCampaigns_AspNetUsers_UserId",
                table: "AdCampaigns",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAdvertisements_AdCampaigns_AdCampaignId",
                table: "CampaignAdvertisements",
                column: "AdCampaignId",
                principalTable: "AdCampaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAdvertisements_Advertisements_AdvertisementId",
                table: "CampaignAdvertisements",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDevices_Panels_PanelId",
                table: "IoTDevices",
                column: "PanelId",
                principalTable: "Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_AspNetUsers_UserId",
                table: "Panels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_Locations_LocationId",
                table: "Panels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Advertisements_AdvertisementId",
                table: "Queues",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Panels_PanelId",
                table: "Queues",
                column: "PanelId",
                principalTable: "Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdCampaigns_AspNetUsers_UserId",
                table: "AdCampaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAdvertisements_AdCampaigns_AdCampaignId",
                table: "CampaignAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAdvertisements_Advertisements_AdvertisementId",
                table: "CampaignAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_IoTDevices_Panels_PanelId",
                table: "IoTDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_AspNetUsers_UserId",
                table: "Panels");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_Locations_LocationId",
                table: "Panels");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Advertisements_AdvertisementId",
                table: "Queues");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Panels_PanelId",
                table: "Queues");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "CampaignAdvertisements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdCampaignId",
                table: "CampaignAdvertisements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AdCampaigns_AspNetUsers_UserId",
                table: "AdCampaigns",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAdvertisements_AdCampaigns_AdCampaignId",
                table: "CampaignAdvertisements",
                column: "AdCampaignId",
                principalTable: "AdCampaigns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAdvertisements_Advertisements_AdvertisementId",
                table: "CampaignAdvertisements",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDevices_Panels_PanelId",
                table: "IoTDevices",
                column: "PanelId",
                principalTable: "Panels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_AspNetUsers_UserId",
                table: "Panels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_Locations_LocationId",
                table: "Panels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Advertisements_AdvertisementId",
                table: "Queues",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Panels_PanelId",
                table: "Queues",
                column: "PanelId",
                principalTable: "Panels",
                principalColumn: "Id");
        }
    }
}
