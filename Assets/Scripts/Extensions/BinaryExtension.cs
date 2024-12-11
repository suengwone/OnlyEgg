using System.IO;
using System.Threading.Tasks;

public static class BinaryExtension
{
    public static async Task SaveBinaryAsync(this byte[] binaries, string filePath)
    {
        using var sourceStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.ReadWrite,
                bufferSize: 4096,
                useAsync: true
            );

        await sourceStream.WriteAsync(binaries, 0, binaries.Length);

        sourceStream.Close();
    }

    public static async Task<byte[]> ReadBinaryAsync(this string filePath)
    {
        if (File.Exists(filePath))
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length);

                fileStream.Close();
                fileStream.Dispose();

                return result;
            }
        }
        else
        {
            return null;
        }
    }
}
