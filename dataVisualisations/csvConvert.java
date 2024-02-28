package AIBilSpelRep.dataVisualisations;

import java.io.*;
import java.util.regex.Matcher;  
import java.util.regex.Pattern; 
import java.util.Arrays;
import java.util.ArrayList;

public class csvConvert {
    public static void main(String[] args) throws IOException {
        String line;
        String[] arr = new String[7];
        String[] arrShort = new String[4];
        String[][] arrArr = new String[200][4];
        int counter = 0;
        try  {  
        FileReader fr = new FileReader(new File("Q://Github//AIBilSpelRep//dataVisualisations//RayTestStrippedLog.txt"));   //reads the file  
        BufferedReader br=new BufferedReader(fr);  //creates a buffering character input stream  
        
        
        while((line=br.readLine())!=null)  
            {  

                line = line.replaceAll("[^-?0-9.]+", " "); 
                arr = line.trim().split(" ");
                arrShort[0] = arr[1];
                arrShort[0] = removeLastChar(arrShort[0]);
                arrShort[1] = arr[2];
                arrShort[2] = arr[4];
                arrShort[2] = removeLastChar(arrShort[2]);
                arrShort[3] = arr[5];
                arrShort[3] = removeLastChar(arrShort[3]);
                
                //System.out.println(arr);
                arr = arrShort;
                printAr(arr);
                counter++;
                for (int i = 0; i<4;i++){
                    arrArr[counter-1][i] = arrShort[i];
                }
            }
            fr.close();    //closes the stream and release the resources  
        }  
        catch(Exception e){
            System.out.print(e);
        }
        
        FileWriter writer = new FileWriter("Q://Github//AIBilSpelRep//dataVisualisations//RayTestLog.csv");
        String csvLine;
        printMatrix(arrArr);
        for (int i = 0; i < 200; i++) {
            csvLine = "";
            for (int j = 0; j < 4; j++){
                csvLine = csvLine+arrArr[i][j]+",";
                
            }
            csvLine = removeLastChar(csvLine);
            csvLine = csvLine+"\n";
            
            writer.write(csvLine);
            writer.flush();
        }
        writer.close();
    }
    public static String removeLastChar(String s) {
        return (s == null || s.length() == 0)
          ? null 
          : (s.substring(0, s.length() - 1));
    }
    public static void printAr(String[] ar){
        //loops through the array and prints all the values
        for (int i = 0; i < ar.length; i++){
            System.out.println(ar[i]);
        }
    }
    public static void printMatrix(String[][] ar1){
        //loops through both layers of the array and prints the values in a square
        for (int i = 0; i < 200; i++){
            for(int j = 0; j <4; j++){
                System.out.print(ar1[i][j]+" ");
            }
            System.out.println();
        }
        System.out.println();
    }
}
