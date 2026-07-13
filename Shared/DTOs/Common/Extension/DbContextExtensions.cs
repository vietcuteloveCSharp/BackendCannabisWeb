using Microsoft.EntityFrameworkCore;
using Shared.Common.Inherited;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Shared.DTOs.Common.Extension
{
	public static class DbContextExtensions
	{
		/// <summary>
		/// Tự động đăng ký các cấu hình EntityTypeConfiguration theo Namespace được chỉ định
		/// </summary>
		public static ModelBuilder ApplyConfigurationsFromNamespace(this ModelBuilder modelBuilder, Assembly assembly, string targetNamespace)
		{
			if (string.IsNullOrEmpty(targetNamespace)) return modelBuilder;

			var configTypes = assembly.GetTypes()
				.Where(t => t.IsClass
						 && !t.IsAbstract
						 && t.Namespace != null
						 && t.Namespace.StartsWith(targetNamespace)
						 && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

			foreach (var type in configTypes)
			{
				dynamic configurationInstance = Activator.CreateInstance(type)!;
				modelBuilder.ApplyConfiguration(configurationInstance);
			}

			return modelBuilder;
		}
		/// <summary>
		/// Tự động cấu hình Query Filter cho các thực thể kế thừa ISoftDelete
		/// </summary>
		public static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
		{
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
				{
					var method = typeof(ModelBuilder)
						.GetMethods()
						.First(m => m.Name == "Entity" && m.IsGenericMethod)
						.MakeGenericMethod(entityType.ClrType);

					method.Invoke(modelBuilder, null);

					var param = Expression.Parameter(entityType.ClrType, "e");
					var prop = Expression.Property(param, nameof(ISoftDelete.IsDeleted));
					var body = Expression.Equal(prop, Expression.Constant(false));
					var lambda = Expression.Lambda(body, param);

					entityType.SetQueryFilter(lambda);
				}
			}

			return modelBuilder;
		}
	}
}
