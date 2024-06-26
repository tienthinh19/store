3/CSA@tG.s/6B3d

Scaffold-DbContext "Name=ConnectionStrings:DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


Add-Migration CustomUserData -context ApplicationDbContext
Update-Database -context ApplicationDbContext