using Service.Interface;

namespace Service.Implementation;

public class FileUploadService : IFileUploadService
{
    // REGISTRIRAJ GO SERVISOT VO PROGRAM.CS
    public async Task<string> UploadFileAsync(byte[] fileBytes, string originalFileName, string folder = "cancellations")
    {
        var uploadsFolder = Path.Combine(
            "wwwroot", "uploads", folder);
        Directory.CreateDirectory(uploadsFolder);

        var extensions = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid()}{extensions}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await File.WriteAllBytesAsync(filePath, fileBytes);

        return $"/uploads/{folder}/{fileName}";
    }
}