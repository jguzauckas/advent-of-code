import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Day1 {
	
	//Reads txt file into String array line by line
	public static String[] readIn(String filename) {
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
		
		String[] strArray = list.toArray(new String[0]);
		return strArray;
	}

	// Calculate the calibration number based on a provided String.
    public static int calibrationNumber(String str) {
        int value = 0;
        str = str.replaceAll("[^0-9]", "");
		value += 10 * Integer.parseInt(str.substring(0, 1));
		value += Integer.parseInt(str.substring(str.length() - 1));
		return value;
    }

    // Get the calibration numbers for each String in the input and return the array of them.
    public static int[] calibrationNumbersPart1(String[] strArray) {
        int[] values = new int[strArray.length];
        for (int i = 0; i < strArray.length; i++) {
            values[i] = calibrationNumber(strArray[i]);
        }
		return values;
    }

	// Replaces words of the digits with the digits themselves.
	// Includes keeping the first/last letters of the digit in case it is included in another digit word.
	public static String part2Modification(String str) {
		String[] numbers = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
		for (int i = 0; i < numbers.length; i++) {
			while (str.contains(numbers[i])) {
				str = str.substring(0, str.indexOf(numbers[i])) + numbers[i].charAt(0) + (i + 1) + numbers[i].charAt(numbers[i].length() - 1) + str.substring(str.indexOf(numbers[i]) + numbers[i].length());
			}
		}
		return str;
	}

	// Same as Part 1, but calls part2Modification on the strings before they are converted to numbers.
	public static int[] calibrationNumbersPart2(String[] strArray) {
		int[] values = new int[strArray.length];
        for (int i = 0; i < strArray.length; i++) {
            values[i] = calibrationNumber(part2Modification(strArray[i]));
        }
		return values;
	}

	// Sum the elements of an integer array.
	public static int sumArray(int[] intArray) {
		int sum = 0;
		for (int i = 0; i < intArray.length; i++) {
			sum += intArray[i];
		}
		return sum;
	}
	
	public static void main(String[] args){
		String[] calibrationDocument = readIn("2023/Inputs/Day1.txt");

        int[] calibrationValuesPart1 = calibrationNumbersPart1(calibrationDocument);

		System.out.println("Part 1: " + sumArray(calibrationValuesPart1));

		int[] calibrationValuesPart2 = calibrationNumbersPart2(calibrationDocument);

		System.out.println("Part 2: " + sumArray(calibrationValuesPart2));
	}

}
