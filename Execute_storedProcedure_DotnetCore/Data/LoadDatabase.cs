////using Microsoft.AspNetCore.Identity;

////namespace Execute_storedProcedure_DotnetCore.Models
////{
////    public class LoadDatabase
////    {
////        public static async Task InsertData(MiApiContext context, UserManager<ApplicationUser> userManager)
////        {
////            if (!userManager.Users.Any())
////            {
////                var user = new ApplicationUser
////                {
////                    UserName = "Francisco",
////                    Identificacion = 28130341,
////                    Nombre = "Francisco",
////                    Apellido1 = "Calvo",
////                    Apellido2 = "Araya",
////                    Email = "administrador.una.ac.cr@gmail.com",
////                };

////                // Use UserManager to create the user with a password
////                var result = await userManager.CreateAsync(user, "UNAContrasenia@2023");

////                // Check if the user creation was successful
////                if (result.Succeeded)
////                {
////                    // Assign the Admin role to the created user
////                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
////                }
////                else
////                {
////                    // Handle errors if user creation fails
////                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
////                    throw new InvalidOperationException($"Error creating default user: {errors}");
////                }
////            }

////            // Save changes to the database context
////            context.SaveChanges();
////        }
////    }
////}



//using Execute_storedProcedure_DotnetCore.Models;
//using Microsoft.AspNetCore.Identity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Execute_storedProcedure_DotnetCore.Data
//{
//    public static class LoadDatabase
//    {
//        public static async Task InitializeDatabaseWithAdminUser(MiApiContext context, UserManager<ApplicationUser> userManager)
//        {
//            if (!context.Users.Any())
//            {
//                var adminUser = new ApplicationUser
//                {
//                    UserName = "administrador.una.ac.cr@gmail.com",
//                    Identificacion = 28130341,
//                    Nombre = "Francisco",
//                    Apellido1 = "Calvo",
//                    Apellido2 = "Araya",
//                    Email = "administrador.una.ac.cr@gmail.com",
//                };

//                // Use UserManager to create the admin user with a password
//                var result = await userManager.CreateAsync(adminUser, "UNAContrasenia@2023");

//                // Check if the user creation was successful
//                if (result.Succeeded)
//                {
//                    // Assign the Admin role to the created user
//                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
//                }
//                else
//                {
//                    // Handle errors if user creation fails
//                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
//                    throw new InvalidOperationException($"Error creating admin user: {errors}");
//                }
//            }
//        }
//    }
//}

