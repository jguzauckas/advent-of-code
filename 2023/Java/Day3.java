import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Day3 {
	
	// Reads txt file into char 2D array.
	public static char[][] readIn(String filename) {
		BufferedReader in;
		String str;

		ArrayList<String> list = new ArrayList<String>();
		try {
			in = new BufferedReader(new FileReader(filename));
			while((str = in.readLine()) != null){
				list.add(str);
			} 
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e1) {
			e1.printStackTrace();
		}
		
		char[][] charArray = new char[list.size()][list.get(0).length()];
		for (int i = 0; i < list.size(); i++) {
			for (int j = 0; j < list.get(i).length(); j++) {
				charArray[i][j] = list.get(i).charAt(j);
			}
		}
		return charArray;
	}

	public static int sumPartNumbers(char[][] schematic) {
		int sum = 0;
		for (int i = 0; i < schematic.length; i++) {
			for (int j = 0; j < schematic[i].length; j++) {
				String currentNumString = "";
				while (j < schematic[i].length && Character.isDigit(schematic[i][j])) {
					currentNumString += schematic[i][j];
					j++;
				}
				if (currentNumString != "") {
					int xmin = Math.max(0, j - currentNumString.length() - 1);
					int xmax = Math.min(j, schematic[i].length - 1);
					int ymin = Math.max(0, i - 1);
					int ymax = Math.min(i + 1, schematic.length - 1);
					for (int k = ymin; k <= ymax; k++) {
						for (int l = xmin; l <= xmax; l++) {
							if (!(l >= j - currentNumString.length() && l < j && k == i)) {
								if (schematic[k][l] != '.') {
									System.out.println(l + "	" + k + "	" + schematic[k][l] + "	" + Integer.parseInt(currentNumString));
									sum += Integer.parseInt(currentNumString);
									l = xmax;
									k = ymax;
								}
							}
						}
					}
				}
			}
		}
		return sum;
	} 
	
	public static void main(String[] args){
		char[][] schematic = readIn("2023/Inputs/Day3.txt");

		System.out.println("Part 1: " + sumPartNumbers(schematic));
	}

}
