namespace RahulTest.Core.Abstractions.Validators
{
    public interface IUploadedFIleInfoValidator
    {
        void Validate(string format, long fileSizeBytes);
    }
}