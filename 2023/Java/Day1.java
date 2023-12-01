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

    public static int calibrationNumber(String str) {
        int value = 0;
        for (int i = 0; i < str.length(); i++) {
            if (Integer.valueOf(str.substring(i, i+1)) == NumberFormatException)
        }
    }

    // Get the calibration numbers for each String in the input and return the array of them.
    public static int[] calibrationNumbers(String[] strArray) {
        int[] values = new int[strArray.length];
        for (int i = 0; i < strArray.length; i++) {
            for (int j = 0; j < strArray[i].length(); j++) {
                if (strArray[i])
            }
        }
    }
	
	public static void main(String[] args){
		String[] calibrationDocument = readIn("input.txt");

        int[] calibrationValues = calibrationNumbers(calibrationDocument);
	}

}
