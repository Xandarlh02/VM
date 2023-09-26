using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class VMTranslations
    {
        public static Dictionary<string, string> operations = new Dictionary<string, string>()
        {
            // Push operations
            {"pushConstant", "@{0}\nD=A\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushArgument", "@ARG\nD=M\n@{0}\nA=D+A\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushLocal", "@LCL\nD=M\n@{0}\nA=D+A\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushStatic", "@fileName.{0}\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushThis", "@THIS\nD=M\n@{0}\nA=D+A\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushThat", "@THAT\nD=M\n@{0}\nA=D+A\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},
            {"pushPointer", "@{0}\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},  // For index 0: THIS, for index 1: THAT
            {"pushTemp", "@5\nD=A\n@{0}\nA=D+A\nD=M\n@SP\nA=M\nM=D\n@SP\nM=M+1"},

            // Pop operations
            {"popArgument", "@ARG\nD=M\n@{0}\nD=D+A\n@13\nM=D\n@SP\nM=M-1\nA=M\nD=M\n@13\nA=M\nM=D"},
            {"popLocal", "@LCL\nD=M\n@{0}\nD=D+A\n@13\nM=D\n@SP\nM=M-1\nA=M\nD=M\n@13\nA=M\nM=D"},
            {"popStatic", "@SP\nM=M-1\nA=M\nD=M\n@fileName.{0}\nM=D"},
            {"popThis", "@THIS\nD=M\n@{0}\nD=D+A\n@13\nM=D\n@SP\nM=M-1\nA=M\nD=M\n@13\nA=M\nM=D"},
            {"popThat", "@THAT\nD=M\n@{0}\nD=D+A\n@13\nM=D\n@SP\nM=M-1\nA=M\nD=M\n@13\nA=M\nM=D"},
            {"popPointer", "@SP\nM=M-1\nA=M\nD=M\n@{0}\nM=D"}, // For address 3: THIS, for address 4: THAT
            {"popTemp", "@5\nD=A\n@{0}\nD=D+A\n@13\nM=D\n@SP\nM=M-1\nA=M\nD=M\n@13\nA=M\nM=D"},

            // Arithmetic and logic operations
            {"add", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=D+M"},
            {"sub", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=M-D"},
            {"neg", "@SP\nA=M-1\nM=-M"},
            {"eq", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=M-D\n@END{0}\nD;JEQ\n@SP\nA=M-1\nM=0\n(END{0})"},
            {"gt", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=M-D\n@END{0}\nD;JGT\n@SP\nA=M-1\nM=0\n(END{0})"},
            {"lt", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=M-D\n@END{0}\nD;JLT\n@SP\nA=M-1\nM=0\n(END{0})"},
            {"and", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=D&M"},
            {"or", "@SP\nM=M-1\nA=M\nD=M\nA=A-1\nM=D|M"},
            {"not", "@SP\nA=M-1\nM=!M"}
        };
    }
}
