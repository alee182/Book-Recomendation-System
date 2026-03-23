using System;
using System.Collections.Generic;

public class RatingParser : Parser
{
	public Dictionary<string, List<int>> RatingDic { get; } = new Dictionary<string, List<int>>();

	public RatingParser()
	{
	}

	public RatingParser(string inputFilePath)
	{
		FileParse(inputFilePath);
		Conversion();
	}

	/// <summary>
	/// Converts ParsedFile into a dictionary of member name to ratings list.
	/// </summary>
	/// <returns>The number of members parsed, or 0 if conversion fails.</returns>
	public override int Conversion()
	{
		RatingDic.Clear();

		if (string.IsNullOrWhiteSpace(ParsedFile))
		{
			return 0;
		}

		string[] lines = ParsedFile.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		if (lines.Length % 2 != 0)
		{
			return 0;
		}

		for (int i = 0; i < lines.Length; i += 2)
		{
			string name = lines[i].Trim();
			string ratingsLine = lines[i + 1].Trim();

			if (string.IsNullOrWhiteSpace(name))
			{
				RatingDic.Clear();
				return 0;
			}

			if (!TryParseRatingsLine(ratingsLine, out List<int>? ratings))
			{
				RatingDic.Clear();
				return 0;
			}

			RatingDic[name] = ratings!;
		}

		return RatingDic.Count;
	}

	/// <summary>
	/// Parses a ratings line containing space-separated integers.
	/// </summary>
	/// <param name="ratingsLine">The line containing one member's ratings.</param>
	/// <param name="ratings">The parsed list of integer ratings when successful.</param>
	/// <returns>True when all tokens are valid integers; otherwise false.</returns>
	private static bool TryParseRatingsLine(string ratingsLine, out List<int>? ratings)
	{
		ratings = null;

		if (string.IsNullOrWhiteSpace(ratingsLine))
		{
			return false;
		}

		string[] tokens = ratingsLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		if (tokens.Length == 0)
		{
			return false;
		}

		List<int> parsedRatings = new List<int>();
		foreach (string token in tokens)
		{
			if (!int.TryParse(token, out int ratingValue))
			{
				return false;
			}

			parsedRatings.Add(ratingValue);
		}

		ratings = parsedRatings;
		return true;
	}
}
