using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Services.Specifications;

internal class GetProductByIdsSpecifications(List<int> ids)
    :BaseSpecification<Product>(propa => ids.Contains(propa.Id));
