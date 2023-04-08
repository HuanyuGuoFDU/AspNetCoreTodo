using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreTodo.Data.Migrations
{
    public partial class AddFTodoItemField0407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "Items",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DueDate",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Items",
                newName: "DueAt");
        }
    }
}
