namespace DemoSample.Core.Abstractions.Validators
{
    public interface IUploadedFIleInfoValidator
    {
        void Validate(string format, long fileSizeBytes);
    }
}