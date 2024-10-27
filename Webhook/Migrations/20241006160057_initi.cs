using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webhook.Migrations
{
    /// <inheritdoc />
    public partial class initi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "ReceivedMessages",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "to",
                table: "ReceivedMessages",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "from",
                table: "ReceivedMessages",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "createtime",
                table: "ReceivedMessages",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "body",
                table: "ReceivedMessages",
                newName: "Body");

            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "ReceivedMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderPhoneNumber",
                table: "ReceivedMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "ReceivedMessages");

            migrationBuilder.DropColumn(
                name: "SenderPhoneNumber",
                table: "ReceivedMessages");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ReceivedMessages",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "ReceivedMessages",
                newName: "to");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "ReceivedMessages",
                newName: "from");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "ReceivedMessages",
                newName: "createtime");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "ReceivedMessages",
                newName: "body");
        }
    }
}
