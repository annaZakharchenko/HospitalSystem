using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLabTestEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    // ✅ Сначала добавляем новые колонки
    migrationBuilder.AddColumn<string>(
        name: "FirstName",
        table: "LabTechnicians",
        type: "longtext",
        nullable: true);  // ✅ Сначала nullable

    migrationBuilder.AddColumn<string>(
        name: "LastName",
        table: "LabTechnicians",
        type: "longtext",
        nullable: true);  // ✅ Сначала nullable

    migrationBuilder.AddColumn<string>(
        name: "FirstName",
        table: "Doctors",
        type: "longtext",
        nullable: true);  // ✅ Сначала nullable

    migrationBuilder.AddColumn<string>(
        name: "LastName",
        table: "Doctors",
        type: "longtext",
        nullable: true);  // ✅ Сначала nullable

    migrationBuilder.AddColumn<int>(
        name: "PatientId",
        table: "LabTests",
        type: "int",
        nullable: false,
        defaultValue: 0);

    migrationBuilder.AddColumn<int>(
        name: "DoctorId",
        table: "LabTestResults",
        type: "int",
        nullable: false,
        defaultValue: 0);

    migrationBuilder.AddColumn<int>(
        name: "PatientId",
        table: "LabTestResults",
        type: "int",
        nullable: false,
        defaultValue: 0);

    // ✅ Копируем данные из старой колонки
    migrationBuilder.Sql(
        "UPDATE `LabTechnicians` SET `LastName` = `LaboratoryName` WHERE `LaboratoryName` IS NOT NULL");

    // ✅ Удаляем старую колонку
    migrationBuilder.DropColumn(
        name: "LaboratoryName",
        table: "LabTechnicians");

    // ✅ Теперь делаем колонки NOT NULL (после того как данные скопированы)
    migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "LabTechnicians",
        type: "longtext",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "longtext",
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "LabTechnicians",
        type: "longtext",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "longtext",
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "Doctors",
        type: "longtext",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "longtext",
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "Doctors",
        type: "longtext",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "longtext",
        oldNullable: true);
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "LabTestResults");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "LabTechnicians");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "LabTechnicians",
                newName: "LaboratoryName");
        }
    }
}
