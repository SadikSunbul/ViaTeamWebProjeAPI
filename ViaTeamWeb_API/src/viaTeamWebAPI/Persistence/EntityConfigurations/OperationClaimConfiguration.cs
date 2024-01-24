using Application;
using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(_seeds);
    }

    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            int id = 0;
            // Admin operasyon yetkisi oluşturulur ve veritabanına eklenir.
            yield return new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin };
            yield return new OperationClaim { Id = ++id, Name = GeneralOperationClaims.StandardUser };

            #region Feature Operation Claims

            // Feature Operation Claims bölümünde tanımlanan yetkiler oluşturulur.
            IEnumerable<Type> featureOperationClaimsTypes = Assembly
                .GetAssembly(typeof(ApplicationServiceRegistration))!
                .GetTypes()
                .Where(
                    type =>
                        (type.Namespace?.Contains("Features") == true)
                        && (type.Namespace?.Contains("Constants") == true)
                        && type.IsClass
                        && type.Name.EndsWith("OperationClaims")
                );
            foreach (Type type in featureOperationClaimsTypes)
            {
                FieldInfo[] typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                IEnumerable<string> typeFieldsValues =
                    typeFields.Select(field => field.GetValue(null)!.ToString()!);

                // Feature Operation Claims sınıflarındaki yetkileri oluşturur.
                IEnumerable<OperationClaim> featureOperationClaimsToAdd = typeFieldsValues.Select(
                    value => new OperationClaim { Id = ++id, Name = value }
                );
                foreach (OperationClaim featureOperationClaim in featureOperationClaimsToAdd)
                    yield return featureOperationClaim;
            }

            #endregion
        }
    }
}