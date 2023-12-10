using Microsoft.EntityFrameworkCore.Migrations;

namespace SGHServer.Application.Migrations._2023._12;

public class Migration_20231209060000 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS Devices(
                Id          int    NOT NULL,
                DeviceName  text   NOT NULL,
                DeviceUid   uid    NOT NULL,
                UserId      int    NULL
                
                CONSTRAINT device_id_pkey PRIMARY KEY (Id),
                CONSTRAINT device_id_fkey FOREIGN KEY (UserId) REFERENCES Users (Id)
            );");

        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS Sensors(
                Id              int       NOT NULL,
                SensorUid       uid       NOT NULL,
                Name            text      NOT NULL,
                LastTimeUpdate  timestamp NOT NULL,
                DeviceId        int       NOT NULL,
                
                CONSTRAINT sensor_id_pkey PRIMARY KEY (Id),
                CONSTRAINT sensor_id_fkey FOREIGN KEY (DeviceId) REFERENCES Sensors (Id)
            );");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable("Devices");
        migrationBuilder.RenameTable("Sensors");
    }
}