using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "play_rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_number = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    price_per_hour = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    has_animator = table.Column<bool>(type: "boolean", nullable: false),
                    theme = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_play_rooms", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_play_rooms_room_number",
                table: "play_rooms",
                column: "room_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "play_rooms");
        }
    }
}
