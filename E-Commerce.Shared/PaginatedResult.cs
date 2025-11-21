using System;

namespace E_Commerce.Shared;

public record PaginatedResult<TResult>(int PageIndex, int Count, int TotalCount, IEnumerable<TResult> Data);
