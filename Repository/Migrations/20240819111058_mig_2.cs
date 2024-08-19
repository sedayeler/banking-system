using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_customers_CustomerId",
                table: "CreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_DebitCard_accounts_AccountId",
                table: "DebitCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitCard",
                table: "DebitCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard");

            migrationBuilder.RenameTable(
                name: "DebitCard",
                newName: "debit_cards");

            migrationBuilder.RenameTable(
                name: "CreditCard",
                newName: "credit_cards");

            migrationBuilder.RenameIndex(
                name: "IX_DebitCard_AccountId",
                table: "debit_cards",
                newName: "IX_debit_cards_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCard_CustomerId",
                table: "credit_cards",
                newName: "IX_credit_cards_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_debit_cards",
                table: "debit_cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_credit_cards",
                table: "credit_cards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_credit_cards_customers_CustomerId",
                table: "credit_cards",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_debit_cards_accounts_AccountId",
                table: "debit_cards",
                column: "AccountId",
                principalTable: "accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_credit_cards_customers_CustomerId",
                table: "credit_cards");

            migrationBuilder.DropForeignKey(
                name: "FK_debit_cards_accounts_AccountId",
                table: "debit_cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_debit_cards",
                table: "debit_cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_credit_cards",
                table: "credit_cards");

            migrationBuilder.RenameTable(
                name: "debit_cards",
                newName: "DebitCard");

            migrationBuilder.RenameTable(
                name: "credit_cards",
                newName: "CreditCard");

            migrationBuilder.RenameIndex(
                name: "IX_debit_cards_AccountId",
                table: "DebitCard",
                newName: "IX_DebitCard_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_credit_cards_CustomerId",
                table: "CreditCard",
                newName: "IX_CreditCard_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitCard",
                table: "DebitCard",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_customers_CustomerId",
                table: "CreditCard",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DebitCard_accounts_AccountId",
                table: "DebitCard",
                column: "AccountId",
                principalTable: "accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
