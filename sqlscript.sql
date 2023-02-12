CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "Username" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230211093540_users1', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "IsVerificationMailSent" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "Users" ADD "IsVerified" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230211105312_users2', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" RENAME COLUMN "Username" TO "Surname";

ALTER TABLE "Users" ADD "Email" text NOT NULL DEFAULT '';

ALTER TABLE "Users" ADD "IsAdmin" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "Users" ADD "Name" text NOT NULL DEFAULT '';

ALTER TABLE "Users" ADD "Role" integer NOT NULL DEFAULT 0;

ALTER TABLE "Users" ADD "RoleText" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230211124641_users4', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" DROP COLUMN "Role";

ALTER TABLE "Users" DROP COLUMN "RoleText";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230211124923_users5', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" DROP COLUMN "IsVerificationMailSent";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230212095550_users6', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "VerifiedAt" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230212112323_users7', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "VerificationToken" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230212132916_users8', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "ResetPasswordToken" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230212133157_users9', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" DROP COLUMN "ResetPasswordToken";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230212135124_users10', '7.0.2');

COMMIT;

