namespace Ecommerce.Shared.DTOs;
public record PaginatedResult<TResult>(int pageSize ,int pageCount , int TotalCount , IEnumerable<TResult> Data);
