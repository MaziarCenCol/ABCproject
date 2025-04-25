using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineOpNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineOpNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationCostHourly = table.Column<double>(type: "float", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAvailableHoursPerDay = table.Column<int>(type: "int", nullable: true),
                    MachineManufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orientation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xtravel = table.Column<double>(type: "float", nullable: true),
                    XtravelInches = table.Column<double>(type: "float", nullable: true),
                    Ytravel = table.Column<double>(type: "float", nullable: true),
                    YtravelInches = table.Column<double>(type: "float", nullable: true),
                    ZtravelDaylight = table.Column<double>(type: "float", nullable: true),
                    ZtravelDaylightInches = table.Column<double>(type: "float", nullable: true),
                    Wtravel = table.Column<double>(type: "float", nullable: true),
                    TableCapacity = table.Column<double>(type: "float", nullable: true),
                    Rpm = table.Column<double>(type: "float", nullable: true),
                    RapidMMmin = table.Column<double>(type: "float", nullable: true),
                    MaxFeedMMmin = table.Column<double>(type: "float", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    ToolChangerCapacity = table.Column<int>(type: "int", nullable: true),
                    ToolHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalibratedAccuracyMM = table.Column<double>(type: "float", nullable: true),
                    PlateLength = table.Column<double>(type: "float", nullable: true),
                    PlateLengthInches = table.Column<double>(type: "float", nullable: true),
                    PlateWidth = table.Column<double>(type: "float", nullable: true),
                    PlateWidthInches = table.Column<double>(type: "float", nullable: true),
                    MaxOpen = table.Column<double>(type: "float", nullable: true),
                    MaxOpenInches = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineOpNameId = table.Column<int>(type: "int", nullable: true),
                    OperationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_MachineOpNames_MachineOpNameId",
                        column: x => x.MachineOpNameId,
                        principalTable: "MachineOpNames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineMachineOpName",
                columns: table => new
                {
                    MachineOpNamesId = table.Column<int>(type: "int", nullable: false),
                    MachinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineMachineOpName", x => new { x.MachineOpNamesId, x.MachinesId });
                    table.ForeignKey(
                        name: "FK_MachineMachineOpName_MachineOpNames_MachineOpNamesId",
                        column: x => x.MachineOpNamesId,
                        principalTable: "MachineOpNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineMachineOpName_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineWeeklyUpTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Weekday = table.Column<int>(type: "int", nullable: false),
                    UpHour = table.Column<int>(type: "int", nullable: false),
                    OperationStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineWeeklyUpTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineWeeklyUpTimes_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineOperationPriority",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    OperationCategoryId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineOperationPriority", x => new { x.MachineId, x.OperationCategoryId });
                    table.ForeignKey(
                        name: "FK_MachineOperationPriority_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineOperationPriority_OperationCategory_OperationCategoryId",
                        column: x => x.OperationCategoryId,
                        principalTable: "OperationCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobNo = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionPercentage = table.Column<double>(type: "float", nullable: true),
                    RequiredProdHours = table.Column<double>(type: "float", nullable: true),
                    ActualProdHours = table.Column<double>(type: "float", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineDownSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DownType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineDownSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineDownSchedules_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineDownSchedules_ScheduleVersion_VersionId",
                        column: x => x.VersionId,
                        principalTable: "ScheduleVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    TaskSeq = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionPercentage = table.Column<double>(type: "float", nullable: false),
                    RequiredProdHours = table.Column<double>(type: "float", nullable: false),
                    ActualProdHours = table.Column<double>(type: "float", nullable: false),
                    OperationComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    MaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMachines",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    OperationCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMachines", x => new { x.TaskId, x.MachineId });
                    table.ForeignKey(
                        name: "FK_TaskMachines_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskMachines_OperationCategory_OperationCategoryId",
                        column: x => x.OperationCategoryId,
                        principalTable: "OperationCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskMachines_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    SubTasks_Total = table.Column<int>(type: "int", nullable: false),
                    SubTask_Number = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduleGenerationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSchedules_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSchedules_ScheduleVersion_VersionId",
                        column: x => x.VersionId,
                        principalTable: "ScheduleVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSchedules_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ProjectId",
                table: "Jobs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineDownSchedules_MachineId",
                table: "MachineDownSchedules",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineDownSchedules_VersionId",
                table: "MachineDownSchedules",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineMachineOpName_MachinesId",
                table: "MachineMachineOpName",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineOperationPriority_OperationCategoryId",
                table: "MachineOperationPriority",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineWeeklyUpTimes_MachineId",
                table: "MachineWeeklyUpTimes",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TaskId",
                table: "Materials",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MachineOpNameId",
                table: "Operations",
                column: "MachineOpNameId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMachines_MachineId",
                table: "TaskMachines",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMachines_OperationCategoryId",
                table: "TaskMachines",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_JobId",
                table: "Tasks",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OperationId",
                table: "Tasks",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedules_MachineId",
                table: "TaskSchedules",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedules_TaskId",
                table: "TaskSchedules",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedules_VersionId",
                table: "TaskSchedules",
                column: "VersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineDownSchedules");

            migrationBuilder.DropTable(
                name: "MachineMachineOpName");

            migrationBuilder.DropTable(
                name: "MachineOperationPriority");

            migrationBuilder.DropTable(
                name: "MachineWeeklyUpTimes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "TaskMachines");

            migrationBuilder.DropTable(
                name: "TaskSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OperationCategory");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "ScheduleVersion");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "MachineOpNames");
        }
    }
}
