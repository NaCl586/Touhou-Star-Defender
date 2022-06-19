using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //char to keycode converter
    public static Dictionary<string, KeyCode> chartoKeycode = new Dictionary<string, KeyCode>()
    {
      //Mouse
      {"LMB",KeyCode.Mouse0},
      {"RMB",KeyCode.Mouse1},
      {"MMB",KeyCode.Mouse2},

      //arrow keys
      {"Up", KeyCode.UpArrow},
      {"Down", KeyCode.DownArrow},
      {"Left", KeyCode.LeftArrow},
      {"Right", KeyCode.RightArrow},

      //command buttons
      {"LShift", KeyCode.LeftShift},
      {"RShift", KeyCode.RightShift},
      {"LCtrl", KeyCode.LeftControl},
      {"RCtrl", KeyCode.RightControl},
      {"LAlt", KeyCode.LeftAlt},
      {"RAlt", KeyCode.RightAlt},
      {"LCommand", KeyCode.LeftCommand},
      {"RCommand", KeyCode.RightCommand},
      {"Tab", KeyCode.Tab},
      {"Space Bar", KeyCode.Space},
      {"Backspace", KeyCode.Backspace},
      {"Delete", KeyCode.Delete},
      {"Insert", KeyCode.Insert},
      {"Page Up", KeyCode.PageUp},
      {"Page Down", KeyCode.PageDown},
      {"Home", KeyCode.Home},
      {"End", KeyCode.End},
      
      //Letters
      {"A", KeyCode.A},
      {"B", KeyCode.B},
      {"C", KeyCode.C},
      {"D", KeyCode.D},
      {"E", KeyCode.E},
      {"F", KeyCode.F},
      {"G", KeyCode.G},
      {"H", KeyCode.H},
      {"I", KeyCode.I},
      {"J", KeyCode.J},
      {"K", KeyCode.K},
      {"L", KeyCode.L},
      {"M", KeyCode.M},
      {"N", KeyCode.N},
      {"O", KeyCode.O},
      {"P", KeyCode.P},
      {"Q", KeyCode.Q},
      {"R", KeyCode.R},
      {"S", KeyCode.S},
      {"T", KeyCode.T},
      {"U", KeyCode.U},
      {"V", KeyCode.V},
      {"W", KeyCode.W},
      {"X", KeyCode.X},
      {"Y", KeyCode.Y},
      {"Z", KeyCode.Z},
  
      //KeyPad Numbers
      {"1", KeyCode.Keypad1},
      {"2", KeyCode.Keypad2},
      {"3", KeyCode.Keypad3},
      {"4", KeyCode.Keypad4},
      {"5", KeyCode.Keypad5},
      {"6", KeyCode.Keypad6},
      {"7", KeyCode.Keypad7},
      {"8", KeyCode.Keypad8},
      {"9", KeyCode.Keypad9},
      {"0", KeyCode.Keypad0},
  
      //Other Symbols
      {"!", KeyCode.Exclaim}, //1
      //{"'", KeyCode.DoubleQuote},
      {"#", KeyCode.Hash}, //3
      {"$", KeyCode.Dollar}, //4
      {"&", KeyCode.Ampersand}, //7
      {"\'", KeyCode.Quote}, //remember the special forward slash rule... this isnt wrong
      {"(", KeyCode.LeftParen}, //9
      {")", KeyCode.RightParen}, //0
      {"*", KeyCode.Asterisk}, //8
      {"+", KeyCode.Plus},
      {",", KeyCode.Comma},
      {"-", KeyCode.Minus},
      {"/", KeyCode.Slash},
      {":", KeyCode.Colon},
      {";", KeyCode.Semicolon},
      {"<", KeyCode.Less},
      {"=", KeyCode.Equals},
      {">", KeyCode.Greater},
      {"?", KeyCode.Question},
      {"@", KeyCode.At}, //2
      {"[", KeyCode.LeftBracket},
      {"\\", KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
      {"]", KeyCode.RightBracket},
      {"^", KeyCode.Caret}, //6
      {"_", KeyCode.Underscore},
      {"`", KeyCode.BackQuote},

      {"Numpad 1", KeyCode.Alpha1},
      {"Numpad 2", KeyCode.Alpha2},
      {"Numpad 3", KeyCode.Alpha3},
      {"Numpad 4", KeyCode.Alpha4},
      {"Numpad 5", KeyCode.Alpha5},
      {"Numpad 6", KeyCode.Alpha6},
      {"Numpad 7", KeyCode.Alpha7},
      {"Numpad 8", KeyCode.Alpha8},
      {"Numpad 9", KeyCode.Alpha9},
      {"Numpad 0", KeyCode.Alpha0},
  
      //INACTIVE since I am using these characters else where
      {"Numpad .", KeyCode.KeypadPeriod},
      {"Numpad /", KeyCode.KeypadDivide},
      {"Numpad *", KeyCode.KeypadMultiply},
      {"Numpad -", KeyCode.KeypadMinus},
      {"Numpad +", KeyCode.KeypadPlus},
      {"Numpad =", KeyCode.KeypadEquals},

      {"Escape", KeyCode.Escape},
    };

    //function to convert keycode into string
    public static string keycodeToChar(KeyCode keyPress)
    {
        foreach (KeyValuePair<string, KeyCode> keybind in Utils.chartoKeycode)
        {
            if (keybind.Value == keyPress)
            {
                return keybind.Key;
            }
        }
        return "???";
    }
}
