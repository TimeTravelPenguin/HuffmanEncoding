#region Title Header

// Name: Phillip Smith
// 
// Solution: HuffmanEncoding
// Project: HuffmanEncoding
// File Name: Program.cs
// 
// Current Data:
// 2020-10-30 4:54 PM
// 
// Creation Date:
// 2020-10-30 4:15 PM

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HuffmanEncoding
{
  public static class Program
  {
    internal class Symbol
    {
      private Symbol _composition;
      public char CharacterSymbol { get; }
      public double Frequency { get; }

      public Symbol(char symbol, double frequency)
      {
        CharacterSymbol = symbol;
        Frequency = frequency;
      }

      public Symbol(char symbol, double frequency, Symbol composition) : this(symbol, frequency)
      {
        _composition = composition;
      }

      public void Branch(Symbol symbol)
      {
        if (_composition is null)
        {
          _composition = symbol;
        }
        else
        {
          _composition.Branch(symbol);
        }
      }
    }

    public static void Main(string[] args)
    {
      if (args is null || args.Length != 1)
      {
        CloseProgram("Please run this application by dragging a file onto the executable...");
      }

      if (args![0] is null)
      {
        CloseProgram("Error: NULL path provided...");
      }

      var fileDir = args[0];
      var validFile = File.Exists(fileDir);

      if (!validFile)
      {
        CloseProgram("The provided file path is not valid...");
      }

      Encode(fileDir);
    }


    private static void CloseProgram(string closeMsg = "")
    {
      Console.WriteLine(closeMsg);
      Console.ReadKey(true);
      Environment.Exit(0);
    }

    private static void Encode(string fileDir)
    {
      using var reader = new StreamReader(fileDir);

      var data = reader.ReadToEnd();

      var huffDict = ConstructDictionary(data);

      var tree = BuildTree(huffDict);

      CloseProgram();
    }

    private static object BuildTree(IDictionary<char, double> huffDict)
    {
      var orderedFreq = huffDict.Select(x => (x.Key, x.Value))
        .OrderBy(x => x.Value)
        .ToList();


      while (orderedFreq.Count > 0)
      {
        
      }
    }

    private static IDictionary<char, double> ConstructDictionary(string data)
    {
      var symbolFreq = new Dictionary<char, double>();

      var len = data.Length;

      foreach (var chr in data)
      {
        if (symbolFreq.ContainsKey(chr))
        {
          symbolFreq[chr] += 1;
        }
        else
        {
          symbolFreq.Add(chr, 1);
        }
      }

      foreach (var (sym, prob) in symbolFreq.Select(x => (x.Key, x.Value)).ToArray())
      {
        symbolFreq[sym] = prob / len;
      }

      return symbolFreq;
    }
  }
}