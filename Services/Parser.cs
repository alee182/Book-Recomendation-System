using System;
using System.IO;

public abstract class Parser
{
	public string ParsedFile { get; set; } = string.Empty;

	/// <summary>
	/// Reads a text file and stores its contents in the ParsedFile property.
	/// </summary>
	/// <param name="inputFilePath">The path to the .txt file that should be read.</param>
	/// <returns>The full text contents of the file.</returns>
	public virtual string FileParse(string inputFilePath)
	{
		if (string.IsNullOrWhiteSpace(inputFilePath))
		{
			throw new ArgumentException("file path can't be empty", nameof(inputFilePath));
		}

		if (!File.Exists(inputFilePath))
		{
			throw new FileNotFoundException("input file path not found", inputFilePath);
		}

		if (!string.Equals(Path.GetExtension(inputFilePath), ".txt", StringComparison.OrdinalIgnoreCase))
		{
			throw new ArgumentException("You can only enter .txt files", nameof(inputFilePath));
		}

		ParsedFile = File.ReadAllText(inputFilePath);
		return ParsedFile;
	}

	/// <summary>
	/// Converts the parsed file contents into parser-specific data.
	/// </summary>
	/// <returns>The number of records successfully converted.</returns>
	public abstract int Conversion();

}
