// Pour les opérations AVEC retour de données
public record ServiceResult<TData>(
    bool IsSuccess,
    TData? Data,
    string Message = "",
    IEnumerable<ServiceError>? Errors = null)
{
    public static ServiceResult<TData> Success(TData data, string message = "")
        => new(true, data, message);

    public static ServiceResult<TData> Failure(string errorMessage, IEnumerable<ServiceError>? errors = null)
        => new(false, default, errorMessage, errors);

    public static implicit operator bool(ServiceResult<TData> result) => result.IsSuccess;
}

// Pour les opérations SANS retour de données
public record ServiceResult(
    bool IsSuccess,
    string Message = "",
    IEnumerable<ServiceError>? Errors = null)
{
    public static ServiceResult Success(string message = "")
        => new(true, message);

    public static ServiceResult Failure(string errorMessage, IEnumerable<ServiceError>? errors = null)
        => new(false, errorMessage, errors);

}

public record ServiceError(string Code, string Description);