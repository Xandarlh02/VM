using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class VMTranslator : IVMTranslator
    {
        public VMTranslator()
        {
        }


        /// <summary>
        /// Translates VM code to Hack Assembly code.
        /// </summary>
        /// <param name="file">Path to the VM file to be translated.</param>
        public void TranslateVmToAsm(string file)
        {
            var vmCode = FileReader.ProcessFile(file);
            List<string> translatedLines = new List<string>();

            foreach (var line in vmCode.Split('\n'))
            {
                string[] parts = line.Trim().Split(' ');

                if (parts.Length < 2) continue;

                string command = parts[0];
                string segment = parts.Length > 1 ? parts[1] : string.Empty;
                int index = parts.Length > 2 && int.TryParse(parts[2], out int number) ? number : -1;

                switch (command)
                {
                    case "push":
                        switch (segment)
                        {
                            case "constant":
                                translatedLines.Add(TranslatePushConstant(index));
                                break;
                            case "argument":
                                translatedLines.Add(TranslatePush("pushArgument", index));
                                break;
                            case "local":
                                translatedLines.Add(TranslatePush("pushLocal", index));
                                break;
                            case "temp":
                                translatedLines.Add(TranslatePush("pushTemp", index));
                                break;
                            case "pointer":
                                translatedLines.Add(TranslatePush("pushPointer", index));
                                break;
                        }
                        break;

                    case "pop":
                        switch (segment)
                        {
                            case "argument":
                                translatedLines.Add(TranslatePop("popArgument", index));
                                break;
                            case "local":
                                translatedLines.Add(TranslatePop("popLocal", index));
                                break;
                            case "temp":
                                translatedLines.Add(TranslatePop("popTemp", index));
                                break;
                            case "pointer":
                                translatedLines.Add(TranslatePop("popPointer", index));
                                break;
                        }
                        break;

                    case "add":
                    case "sub":
                    case "neg":
                    case "eq":
                    case "gt":
                    case "lt":
                    case "and":
                    case "or":
                    case "not":
                        translatedLines.Add(VMTranslations.operations[command]);
                        break;
                }
            }
            File.WriteAllLines("C:\\Users\\Alexandar Lackovic\\Desktop\\skole\\nand2tetris\\nand2tetris\\projects\\07\\StackArithmetic\\SimpleAdd\\SimpleAdd.asm", translatedLines);
        }

        /// <summary>
        /// Translates a 'push' VM command to assembly.
        /// </summary>
        /// <param name="operation">The type of 'push' operation.</param>
        /// <param name="index">The segment index.</param>
        /// <returns>Assembly code corresponding to the 'push' command.</returns>
        public string TranslatePush(string operation, int index)
        {
            return string.Format(VMTranslations.operations[operation], index);
        }

        /// <summary>
        /// Translates a 'pop' VM command to assembly.
        /// </summary>
        /// <param name="operation">The type of 'pop' operation.</param>
        /// <param name="index">The segment index.</param>
        /// <returns>Assembly code corresponding to the 'pop' command.</returns>
        public string TranslatePop(string operation, int index)
        {
            return string.Format(VMTranslations.operations[operation], index);
        }

        /// <summary>
        /// Translates a 'push constant' VM command to assembly.
        /// </summary>
        /// <param name="value">The constant value to be pushed.</param>
        /// <returns>Assembly code corresponding to the 'push constant' command.</returns>
        public string TranslatePushConstant(int value)
        {
            return string.Format(VMTranslations.operations["pushConstant"], value);
        }
    }
}
