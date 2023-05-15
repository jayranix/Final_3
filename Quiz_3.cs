using System;
using System.IO;

class Program {
    static void Main() {
        Console.Write("Enter the input image file path: ");
        string inputImagePath = Console.ReadLine();
        Console.Write("Enter the convolution kernel file path: ");
        string kernelFilePath = Console.ReadLine();
        Console.Write("Enter the output image file path: ");
        string outputImagePath = Console.ReadLine();

        double[,] imageData = ReadImageDataFromFile(inputImagePath);

        double[,] kernelData = ReadImageDataFromFile(kernelFilePath);

        double[,] convolvedData = Convolve(imageData, kernelData);

        WriteImageDataToFile(outputImagePath, convolvedData);
    }

    static double[,] ReadImageDataFromFile(string filePath) {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open))) {
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            double[,] imageData = new double[height, width];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    imageData[y, x] = reader.ReadDouble();
                }
            }

            return imageData;
        }
    }

    static void WriteImageDataToFile(string filePath, double[,] imageData) {
        int height = imageData.GetLength(0);
        int width = imageData.GetLength(1);

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create))) {
            writer.Write(width);
            writer.Write(height);

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    writer.Write(imageData[y, x]);
                }
            }
        }
    }

    static double[,] Convolve(double[,] imageData, double[,] kernelData) {
        int imageHeight = imageData.GetLength(0);
        int imageWidth = imageData.GetLength(1);
        int kernelHeight = kernelData.GetLength(0);
        int kernelWidth = kernelData.GetLength(1);