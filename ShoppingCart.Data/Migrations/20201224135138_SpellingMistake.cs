﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class SpellingMistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sock",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Sock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
