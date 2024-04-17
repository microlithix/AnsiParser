namespace Microlithix.Text.Ansi;

// Extension method for converting a character
// into a hexadecimal string representation.
// Useful for displaying non-printable characters.
internal static class CharExtensions {
	public static string ToHexString(this char ch) {
		int value = Convert.ToInt32(ch);
		string formatString = value < 256 ? "X2" : "X4";
		return $"0x{value.ToString(formatString)}";
	}
}
