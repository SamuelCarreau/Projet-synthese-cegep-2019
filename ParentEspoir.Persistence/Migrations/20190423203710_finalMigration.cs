using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParentEspoir.Persistence.Migrations
{
    public partial class finalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildrenAgeBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildrenAgeBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CitizenStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeardOfUsFroms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeardOfUsFroms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalCustodies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCustodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    CommandName = table.Column<string>(nullable: false),
                    CommandJSON = table.Column<string>(nullable: false),
                    Information = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schoolings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schoolings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Sexs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillToDevelops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillToDevelops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportGroups",
                columns: table => new
                {
                    SupportGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportGroups", x => x.SupportGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TransportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteeringTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteeringTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 25, nullable: true),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearlyIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyIncomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SupportGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_SupportGroups_SupportGroupId",
                        column: x => x.SupportGroupId,
                        principalTable: "SupportGroups",
                        principalColumn: "SupportGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileNumber = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SuppressionDate = table.Column<DateTime>(nullable: true),
                    InscriptionDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 75, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 75, nullable: true),
                    Province = table.Column<string>(maxLength: 75, nullable: true),
                    Country = table.Column<string>(maxLength: 75, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 20, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    SupportGroupId = table.Column<int>(nullable: true),
                    ReferenceById = table.Column<int>(nullable: true),
                    HeardOfUsFromId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_HeardOfUsFroms_HeardOfUsFromId",
                        column: x => x.HeardOfUsFromId,
                        principalTable: "HeardOfUsFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_ReferenceTypes_ReferenceById",
                        column: x => x.ReferenceById,
                        principalTable: "ReferenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_SupportGroups_SupportGroupId",
                        column: x => x.SupportGroupId,
                        principalTable: "SupportGroups",
                        principalColumn: "SupportGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkshopName = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    WorkshopDescription = table.Column<string>(nullable: true),
                    WorkshopTypeId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.WorkshopId);
                    table.ForeignKey(
                        name: "FK_Workshops_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workshops_WorkshopTypes_WorkshopTypeId",
                        column: x => x.WorkshopTypeId,
                        principalTable: "WorkshopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerActivation",
                columns: table => new
                {
                    CustomerActivationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    IsActiveSince = table.Column<DateTime>(nullable: false),
                    IsInactiveSince = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerActivation", x => x.CustomerActivationId);
                    table.ForeignKey(
                        name: "FK_CustomerActivation_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDescriptions",
                columns: table => new
                {
                    CustomerDescriptionId = table.Column<int>(nullable: false),
                    SexId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    MaritalStatusId = table.Column<int>(nullable: true),
                    CitizenStatusId = table.Column<int>(nullable: true),
                    FamilyTypeId = table.Column<int>(nullable: true),
                    LanguageSpokenId = table.Column<int>(nullable: true),
                    HomeTypeId = table.Column<int>(nullable: true),
                    TransportTypeId = table.Column<int>(nullable: true),
                    SchoolingId = table.Column<int>(nullable: true),
                    IncomeSourceId = table.Column<int>(nullable: true),
                    AvailabilityId = table.Column<int>(nullable: true),
                    YearlyIncomeId = table.Column<int>(nullable: true),
                    LegalCustodyId = table.Column<int>(nullable: true),
                    WantsToBecomeMember = table.Column<bool>(nullable: true),
                    ChildrenCount = table.Column<int>(nullable: false, defaultValue: 0),
                    HasMentalHealthDiagnostic = table.Column<bool>(nullable: false),
                    HasBeenHospitalisedInPsychiatry = table.Column<bool>(nullable: false),
                    HasContactWithDPJnow = table.Column<bool>(nullable: false),
                    WillParticipateToHelpingGroup = table.Column<bool>(nullable: false),
                    HasContactWithDPJinPast = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDescriptions", x => x.CustomerDescriptionId);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_CitizenStatuses_CitizenStatusId",
                        column: x => x.CitizenStatusId,
                        principalTable: "CitizenStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Customers_CustomerDescriptionId",
                        column: x => x.CustomerDescriptionId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_FamilyTypes_FamilyTypeId",
                        column: x => x.FamilyTypeId,
                        principalTable: "FamilyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_HomeTypes_HomeTypeId",
                        column: x => x.HomeTypeId,
                        principalTable: "HomeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_IncomeSources_IncomeSourceId",
                        column: x => x.IncomeSourceId,
                        principalTable: "IncomeSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Languages_LanguageSpokenId",
                        column: x => x.LanguageSpokenId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_LegalCustodies_LegalCustodyId",
                        column: x => x.LegalCustodyId,
                        principalTable: "LegalCustodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Schoolings_SchoolingId",
                        column: x => x.SchoolingId,
                        principalTable: "Schoolings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_Sexs_SexId",
                        column: x => x.SexId,
                        principalTable: "Sexs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_TransportTypes_TransportTypeId",
                        column: x => x.TransportTypeId,
                        principalTable: "TransportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDescriptions_YearlyIncomes_YearlyIncomeId",
                        column: x => x.YearlyIncomeId,
                        principalTable: "YearlyIncomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentName = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    VolunteeringHourCountByMonth = table.Column<int>(nullable: false),
                    AmountByMonth = table.Column<decimal>(nullable: false),
                    SubscriptionDate = table.Column<DateTime>(nullable: true),
                    RenewalDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_Customers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NoteName = table.Column<string>(maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    SupervisorName = table.Column<string>(nullable: true),
                    SupervisorTitle = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    NoteTypeId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_NoteTypes_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "NoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    WorkshopTypeId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objectives_WorkshopTypes_WorkshopTypeId",
                        column: x => x.WorkshopTypeId,
                        principalTable: "WorkshopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteerings",
                columns: table => new
                {
                    VolunteeringId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 75, nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    VolonteeringTypeId = table.Column<int>(nullable: true),
                    HourCount = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Acknowledgment = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteerings", x => x.VolunteeringId);
                    table.ForeignKey(
                        name: "FK_Volunteerings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteerings_VolunteeringTypes_VolonteeringTypeId",
                        column: x => x.VolonteeringTypeId,
                        principalTable: "VolunteeringTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seances",
                columns: table => new
                {
                    SeanceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeanceName = table.Column<string>(nullable: true),
                    SeanceDate = table.Column<DateTime>(nullable: false),
                    SeanceTimeSpan = table.Column<TimeSpan>(nullable: false),
                    SeanceDescription = table.Column<string>(nullable: true),
                    WorkshopId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seances", x => x.SeanceId);
                    table.ForeignKey(
                        name: "FK_Seances_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "WorkshopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerChildrenAgeBrackets",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    AgeBracketId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerChildrenAgeBrackets", x => new { x.AgeBracketId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CustomerChildrenAgeBrackets_ChildrenAgeBrackets_AgeBracketId",
                        column: x => x.AgeBracketId,
                        principalTable: "ChildrenAgeBrackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerChildrenAgeBrackets_CustomerDescriptions_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSkillToDevelops",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSkillToDevelops", x => new { x.CustomerId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CustomerSkillToDevelops_CustomerDescriptions_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSkillToDevelops_SkillToDevelops_SkillId",
                        column: x => x.SkillId,
                        principalTable: "SkillToDevelops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSocialServices",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    SocialServiceId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSocialServices", x => new { x.CustomerId, x.SocialServiceId });
                    table.ForeignKey(
                        name: "FK_CustomerSocialServices_CustomerDescriptions_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSocialServices_SocialServices_SocialServiceId",
                        column: x => x.SocialServiceId,
                        principalTable: "SocialServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnalFollowUps",
                columns: table => new
                {
                    PersonnalFollowUpId = table.Column<int>(nullable: false),
                    MeetingCount = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnalFollowUps", x => x.PersonnalFollowUpId);
                    table.ForeignKey(
                        name: "FK_PersonnalFollowUps_CustomerDescriptions_PersonnalFollowUpId",
                        column: x => x.PersonnalFollowUpId,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferedDays",
                columns: table => new
                {
                    Day = table.Column<int>(nullable: false),
                    CustomerDescriptionID = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferedDays", x => new { x.CustomerDescriptionID, x.Day });
                    table.ForeignKey(
                        name: "FK_PreferedDays_CustomerDescriptions_CustomerDescriptionID",
                        column: x => x.CustomerDescriptionID,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pregnancies",
                columns: table => new
                {
                    PregnancyId = table.Column<int>(nullable: false),
                    ChildBirthExpectedDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregnancies", x => x.PregnancyId);
                    table.ForeignKey(
                        name: "FK_Pregnancies_CustomerDescriptions_PregnancyId",
                        column: x => x.PregnancyId,
                        principalTable: "CustomerDescriptions",
                        principalColumn: "CustomerDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    SeanceId = table.Column<int>(nullable: false),
                    WorkshopId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    NbHourLate = table.Column<TimeSpan>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participants_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Seances_SeanceId",
                        column: x => x.SeanceId,
                        principalTable: "Seances",
                        principalColumn: "SeanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "WorkshopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SupportGroupId",
                table: "AspNetUsers",
                column: "SupportGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerActivation_CustomerId",
                table: "CustomerActivation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChildrenAgeBrackets_CustomerId",
                table: "CustomerChildrenAgeBrackets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_AvailabilityId",
                table: "CustomerDescriptions",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_CitizenStatusId",
                table: "CustomerDescriptions",
                column: "CitizenStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_FamilyTypeId",
                table: "CustomerDescriptions",
                column: "FamilyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_HomeTypeId",
                table: "CustomerDescriptions",
                column: "HomeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_IncomeSourceId",
                table: "CustomerDescriptions",
                column: "IncomeSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_LanguageSpokenId",
                table: "CustomerDescriptions",
                column: "LanguageSpokenId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_LegalCustodyId",
                table: "CustomerDescriptions",
                column: "LegalCustodyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_MaritalStatusId",
                table: "CustomerDescriptions",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_ParentId",
                table: "CustomerDescriptions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_SchoolingId",
                table: "CustomerDescriptions",
                column: "SchoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_SexId",
                table: "CustomerDescriptions",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_TransportTypeId",
                table: "CustomerDescriptions",
                column: "TransportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDescriptions_YearlyIncomeId",
                table: "CustomerDescriptions",
                column: "YearlyIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_HeardOfUsFromId",
                table: "Customers",
                column: "HeardOfUsFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ReferenceById",
                table: "Customers",
                column: "ReferenceById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SupportGroupId",
                table: "Customers",
                column: "SupportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSkillToDevelops_SkillId",
                table: "CustomerSkillToDevelops",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSocialServices_SocialServiceId",
                table: "CustomerSocialServices",
                column: "SocialServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CustomerId",
                table: "Documents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerId",
                table: "Notes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_CustomerId",
                table: "Objectives",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_WorkshopTypeId",
                table: "Objectives",
                column: "WorkshopTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CustomerId",
                table: "Participants",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_SeanceId",
                table: "Participants",
                column: "SeanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_WorkshopId",
                table: "Participants",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_WorkshopId",
                table: "Seances",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteerings_CustomerId",
                table: "Volunteerings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteerings_VolonteeringTypeId",
                table: "Volunteerings",
                column: "VolonteeringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_SessionId",
                table: "Workshops",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_WorkshopTypeId",
                table: "Workshops",
                column: "WorkshopTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerActivation");

            migrationBuilder.DropTable(
                name: "CustomerChildrenAgeBrackets");

            migrationBuilder.DropTable(
                name: "CustomerSkillToDevelops");

            migrationBuilder.DropTable(
                name: "CustomerSocialServices");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "PersonnalFollowUps");

            migrationBuilder.DropTable(
                name: "PreferedDays");

            migrationBuilder.DropTable(
                name: "Pregnancies");

            migrationBuilder.DropTable(
                name: "Volunteerings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChildrenAgeBrackets");

            migrationBuilder.DropTable(
                name: "SkillToDevelops");

            migrationBuilder.DropTable(
                name: "SocialServices");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "NoteTypes");

            migrationBuilder.DropTable(
                name: "Seances");

            migrationBuilder.DropTable(
                name: "CustomerDescriptions");

            migrationBuilder.DropTable(
                name: "VolunteeringTypes");

            migrationBuilder.DropTable(
                name: "Workshops");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "CitizenStatuses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "FamilyTypes");

            migrationBuilder.DropTable(
                name: "HomeTypes");

            migrationBuilder.DropTable(
                name: "IncomeSources");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LegalCustodies");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Schoolings");

            migrationBuilder.DropTable(
                name: "Sexs");

            migrationBuilder.DropTable(
                name: "TransportTypes");

            migrationBuilder.DropTable(
                name: "YearlyIncomes");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "WorkshopTypes");

            migrationBuilder.DropTable(
                name: "HeardOfUsFroms");

            migrationBuilder.DropTable(
                name: "ReferenceTypes");

            migrationBuilder.DropTable(
                name: "SupportGroups");
        }
    }
}
