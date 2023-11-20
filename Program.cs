internal class Program
{
    private static void Main(string[] args)
    {
        bool status = true;
        do
        {
            Console.WriteLine("Enter a number from the following options: ");
            Console.WriteLine("Enter other numbers to Exit");
            Console.WriteLine("1. Parity Checking");
            Console.WriteLine("2. BCC");
            Console.WriteLine("3. Verify BCC");
            Console.WriteLine("4. LRC with Parity");
            int choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 1:
                    ExecuteParityChecking();
                    break;
                case 2:
                    ExecuteBCC();
                    break;
                case 3:
                    ExecuteBCCVerifier();
                    break;
                case 4:
                    ExecuteLRCWithParity();
                    break;
                default:
                    // Exit the program
                    status = false;
                    break;
            } 
        } while (status);
    }

    // Function to check parity and error status of a set of bits
    static void ExecuteParityChecking()
    {
        try
        {
            Console.WriteLine("Enter the number of bits: ");
            int[] bits = new int[int.Parse(Console.ReadLine()!)];
            Console.WriteLine("Enter the bits: ");
            bits = Console.ReadLine()!.Select(c => int.Parse(c.ToString())).ToArray();
            Console.WriteLine("Parity type: " + CheckParity(bits) + "\n");
        }
        catch (Exception ex)
        {

            Console.WriteLine("Invalid Input", ex);
        }
    }

    static void ExecuteBCC()
    {
        Console.WriteLine("Enter a message: ");
        string message = Console.ReadLine()!;
        byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);

        List<string> binaryString = new List<string>();

        foreach (var messageByte in messageBytes)
        {
            binaryString.Add(Convert.ToString(messageByte, 2).PadLeft(8, '0'));
        }

        Console.WriteLine("Letter\tASCII in Binary");
        foreach (var binary in binaryString)
        {
            Console.WriteLine(message[binaryString.IndexOf(binary)] + "\t" + binary);
        }
        Console.WriteLine("BCC\t" + CalculateBCC(binaryString.ToArray()) + "\n");
    }

    static void ExecuteBCCVerifier()
    {
        Console.WriteLine("Enter number of bytes: ");
        int numberOfBytes = int.Parse(Console.ReadLine()!);
        List<string> binaryString = new List<string>();
        for (int i = 0; i < numberOfBytes; i++)
        {
            Console.WriteLine("Enter byte[" + i + "]: ");
            binaryString.Add(Console.ReadLine()!);
        }
        Console.WriteLine("Enter your computed BCC: ");
        string computedBCC = Console.ReadLine()!;
        if (VerifyBCC(computedBCC, binaryString.ToArray()))
        {
            Console.WriteLine("Your Computed BCC has no errors.");
        }
        else
        {
            Console.WriteLine("Your Computed BCC has an error.");
        }
        Console.WriteLine();
    }

    static void ExecuteLRCWithParity()
    {
        Console.WriteLine("Enter number of bytes: ");
        int numberOfBytes = int.Parse(Console.ReadLine()!);
        List<string> binaryString = new List<string>();
        for (int i = 0; i < numberOfBytes; i++)
        {
            Console.WriteLine("Enter byte[" + i + "]: ");
            binaryString.Add(Console.ReadLine()!);
        }
        string computedBCC = CalculateBCC(binaryString.ToArray());
        binaryString.Add(computedBCC);

        Console.WriteLine("Letter\tASCII in Binary\tEVEN\tODD");
        foreach (var binary in binaryString)
        {
            int[] bits = binary.Select(c => int.Parse(c.ToString())).ToArray();
            if (binaryString.IndexOf(binary) != binaryString.Count - 1)
            {
                Console.Write((char)Convert.ToInt32(binary, 2) + "\t" + binary + "\t");
            }
            else
            {
                Console.Write("BCC\t" + binary + "\t");
            }

            Console.Write(CheckParity(bits).Equals("EVEN") ? "0\t1\n" : "1\t0\n");
        }
    }

    static string CheckParity(int[] bits)
    {
        int onesCounter = 0;
        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i] == 1)
            {
                onesCounter++;
            }
        }

        return onesCounter % 2 == 0 ? "EVEN" : "ODD";
    }

    static string CalculateBCC(string[] binaryString)
    {
        int onesCounter = 0;
        string bcc = "";
        for (int i = 0; i < binaryString[0].Length; i++)
        {
            for (int j = 0; j < binaryString.Length; j++)
            {
                if (binaryString[j][i] == '1')
                {
                    onesCounter++;
                }
            }

            bcc += onesCounter % 2 == 0 ? "0" : "1";
            onesCounter = 0;
        }

        return bcc;
    }

    static bool VerifyBCC(string computedBCC, string[] binaryString)
    {
        return computedBCC.Equals(CalculateBCC(binaryString));
    }
}