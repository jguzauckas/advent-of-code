/*
 * Solves the problem offered by 2020 Advent of Code Day 1
 * Goal was to read in the file input and utilize methods to find the result.
 * 
 * Author: John Guzauckas
 * Version: 09/26/2021
 * 
 */

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Day1 {
	
	//Reads txt file into integer array line by line
	public static Integer[] readIn(String filename) {
		BufferedReader in;
		String str;

		ArrayList<Integer> list = new ArrayList<Integer>();
		try {
			in = new BufferedReader(new FileReader(filename));
			while((str = in.readLine()) != null){
				list.add(Integer.parseInt(str));
			} 
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		
		Integer[] intArray = list.toArray(new Integer[0]);
		return intArray;
	}
	
	//Finds two integers that add up to 2020 in array and then multiply them for return
	public static void twoSum2020(Integer[] arr) {
		for(int i = 0; i < arr.length - 1; i++) {
			for(int j = 0; j < arr.length - 1; j++) {
				if(i != j && arr[i] + arr[j] == 2020) {
					System.out.println("Two numbers that sum to 2020 then multiplied are: " + (arr[i] * arr[j]));
					return;
				}
			}
		}
		System.out.println("This set of integers did not have a pair that add to 2020.");
	}
	
	//Finds three integers that add up to 2020 in array and then multiply them for return
	public static void threeSum2020(Integer[] arr) {
		for(int i = 0; i < arr.length - 1; i++) {
			for(int j = 0; j < arr.length - 1; j++) {
				for(int k = 0; k < arr.length - 1; k++) {
					if((i != j && j != k) && arr[i] + arr[j] + arr[k]== 2020) {
						System.out.println("Two numbers that sum to 2020 then multiplied are: " + (arr[i] * arr[j] * arr[k]));
						return;
					}
				}
			}
		}
		System.out.println("This set of integers did not have a set of three that add to 2020.");
	}
	
	public static void main(String[] args){
		Integer[] expenses = readIn("input.txt");
		
		twoSum2020(expenses);
		
		threeSum2020(expenses);
	}

}
