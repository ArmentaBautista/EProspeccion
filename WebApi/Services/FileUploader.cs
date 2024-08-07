using WebApi.Services;

namespace WebApi.Services;

public class FileUploader : IFileUploader
{
	private readonly ILogger<FileUploader> _logger;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public FileUploader(ILogger<FileUploader> logger, IWebHostEnvironment webHostEnvironment)
	{
		_logger = logger;
		_webHostEnvironment = webHostEnvironment;
	}

	public async Task<string> UploadFileAsync(string? base64Imagen, string? archivo)
	{
		if (string.IsNullOrEmpty(base64Imagen) || string.IsNullOrEmpty(archivo))
		{
			return string.Empty;
		}

		try
		{
			var carpeta = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
			if (!Directory.Exists(carpeta))
				Directory.CreateDirectory(carpeta);

			var bytes = Convert.FromBase64String(base64Imagen);

			var rutaCompleta = Path.Combine(carpeta, archivo);

			await using var fileStream = new FileStream(rutaCompleta, FileMode.Create);
			await fileStream.WriteAsync(bytes, 0, bytes.Length);

			return $"/uploads/{archivo}";
		}
		catch (Exception ex)
		{
			_logger.LogCritical(ex, "Error al subir el archivo {archivo}", archivo);
			return string.Empty;
		}
	}
}