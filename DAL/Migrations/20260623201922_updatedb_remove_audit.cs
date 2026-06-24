using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedb_remove_audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityChanges_AuditLogs_AuditLogId",
                table: "EntityChanges");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_AccessToken",
                schema: "Users",
                table: "UserSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityChanges",
                table: "EntityChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                schema: "Users",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Audit",
                table: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "EntityChanges",
                newName: "EntityChange");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                schema: "Audit",
                newName: "AuditLog");

            migrationBuilder.RenameIndex(
                name: "IX_EntityChanges_AuditLogId",
                table: "EntityChange",
                newName: "IX_EntityChange_AuditLogId");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLog",
                newName: "IX_AuditLog_UserId");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                schema: "Users",
                table: "UserSessions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserSessionId",
                schema: "Users",
                table: "RefreshTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                schema: "Orders",
                table: "OrderItems",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityChange",
                table: "EntityChange",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLog",
                table: "AuditLog",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_SessionId",
                schema: "Users",
                table: "UserSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserSessionId",
                schema: "Users",
                table: "RefreshTokens",
                column: "UserSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Users_UserId",
                table: "AuditLog",
                column: "UserId",
                principalSchema: "Users",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityChange_AuditLog_AuditLogId",
                table: "EntityChange",
                column: "AuditLogId",
                principalTable: "AuditLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_UserSessions_UserSessionId",
                schema: "Users",
                table: "RefreshTokens",
                column: "UserSessionId",
                principalSchema: "Users",
                principalTable: "UserSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLog_Users_UserId",
                table: "AuditLog");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityChange_AuditLog_AuditLogId",
                table: "EntityChange");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_UserSessions_UserSessionId",
                schema: "Users",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_SessionId",
                schema: "Users",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserSessionId",
                schema: "Users",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityChange",
                table: "EntityChange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLog",
                table: "AuditLog");

            migrationBuilder.DropColumn(
                name: "SessionId",
                schema: "Users",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "UserSessionId",
                schema: "Users",
                table: "RefreshTokens");

            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.RenameTable(
                name: "EntityChange",
                newName: "EntityChanges");

            migrationBuilder.RenameTable(
                name: "AuditLog",
                newName: "AuditLogs",
                newSchema: "Audit");

            migrationBuilder.RenameIndex(
                name: "IX_EntityChange_AuditLogId",
                table: "EntityChanges",
                newName: "IX_EntityChanges_AuditLogId");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLog_UserId",
                schema: "Audit",
                table: "AuditLogs",
                newName: "IX_AuditLogs_UserId");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                schema: "Users",
                table: "UserSessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                schema: "Orders",
                table: "OrderItems",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Audit",
                table: "AuditLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "Audit",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Audit",
                table: "AuditLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                schema: "Audit",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Audit",
                table: "AuditLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Audit",
                table: "AuditLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "Audit",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityChanges",
                table: "EntityChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                schema: "Audit",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_AccessToken",
                schema: "Users",
                table: "UserSessions",
                column: "AccessToken");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                schema: "Audit",
                table: "AuditLogs",
                column: "UserId",
                principalSchema: "Users",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityChanges_AuditLogs_AuditLogId",
                table: "EntityChanges",
                column: "AuditLogId",
                principalSchema: "Audit",
                principalTable: "AuditLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
