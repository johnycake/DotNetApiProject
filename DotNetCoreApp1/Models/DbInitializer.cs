using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApp1;

public class DbInitializer
{
    private readonly ModelBuilder modelBuilder;
    public DbInitializer(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        var userGuid = Guid.NewGuid();
        modelBuilder.Entity<UserDto>().HasData(
            new UserDto(){ 
            UserId = userGuid,
            UserName = "Admin",
            FirstName = "Iam",
            Surname = "BigBoss",
            RoleId = "Admin"
            }
        );

        modelBuilder.Entity<PasswordDto>().HasData(
            new PasswordDto(){ 
            UserId = userGuid,
            PasswordId = Guid.NewGuid(),
            PasswordValue = "AdminHeslo123#"
            }
        );
    }
}
