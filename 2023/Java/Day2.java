import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

class Game {
	private int id;
	private String[] colorNames = new String[]{"red", "green", "blue"};
	private int[][] colors;

	// Uses the raw input String to collect data about the provided game.
	public Game(String input) {
		input = input.substring(5);
		id = Integer.parseInt(input.substring(0, input.indexOf(":")));
		input = input.substring(input.indexOf(":") + 2);
		int count = 1;
		for (int i = 0; i < input.length(); i++) {
			if (input.substring(i, i + 1).equals(";")) {
				count++;
			}
		}
		colors = new int[colorNames.length][count];
		for (int i = 0; input.length() > 0; i++) {
			String[] sample;
			if (input.indexOf(";") == -1) {
				sample = input.split(", ");
				input = "";
			} else {
				sample = input.substring(0, input.indexOf(";")).split(", ");
				input = input.substring(input.indexOf(";") + 2);
			}
			
			for (int j = 0; j < sample.length; j++) {
				for (int k = 0; k < colorNames.length; k++) {
					if (sample[j].contains(colorNames[k])) {
						colors[k][i] = Integer.parseInt(sample[j].substring(0, sample[j].indexOf(" ")));
					}
				}
			}
		}
	}

	public int getID() {
		return id;
	}

	// Determines if the game is possible by evaluating if any of the drawings go over the maximum values for each color provided.
	public boolean possibleGame(int[] maxColors) {
		for (int i = 0; i < colors.length; i++) {
			for (int j = 0; j < colors[i].length; j++) {
				if (colors[i][j] > maxColors[i]) {
					return false;
				}
			}
		}
		return true;
	}

	// Establishes the power of the game by multiplying the minimum number of each color of balls present from the drawings.
	public int power() {
		int power = 1;
		for (int i = 0; i < colors.length; i++) {
			int minColor = 0;
			for (int j = 0; j < colors[i].length; j++) {
				if (colors[i][j] > minColor) {
					minColor = colors[i][j];
				}
			}
			power *= minColor;
		}
		return power;
	}
}

public class Day2 {
	
	//Reads txt file into String array line by line
	public static Game[] readIn(String filename) {
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
		
		Game[] gameArray = new Game[list.size()];
		for (int i = 0; i < list.size(); i++) {
			gameArray[i] = new Game(list.get(i));
		}
		return gameArray;
	}

	// Goes through each game and sums the IDs when the game is possible based on the sample drawings.
	public static int possibleGameSum(Game[] games) {
		int total = 0;
		for (int i = 0; i < games.length; i++) {
			if (games[i].possibleGame(new int[]{12, 13, 14})) {
				total += games[i].getID();
			}
		}
		return total;
	}

	// Goes through each game and sums the powers.
	public static int totalGamePower(Game[] games) {
		int total = 0;
		for (int i = 0; i < games.length; i++) {
			total += games[i].power();
		}
		return total;
	}
	
	public static void main(String[] args){
		Game[] games = readIn("2023/Inputs/Day2.txt");

		System.out.println("Part 1: " + possibleGameSum(games));

		System.out.println("Part 2: " + totalGamePower(games));
	}
}
