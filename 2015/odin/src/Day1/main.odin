package day1

import "core:fmt"
import "core:os"
import "core:strings"
sample: bool = false;
path_main: string = "input.txt";
path_sample: string = "input_sample.txt";
main :: proc() 
{
  P1();
  P2();
}

P1 :: proc()
{
  path: string;
  if (sample)
  {
    path = path_sample;
  } else
  {
    path = path_main;
  }
  arr: []byte;
  arr , _ = os.read_entire_file(path, context.allocator);
  defer delete(arr);
  sum := 0;
  for i: int = 0; i < len(arr); i+=1
  {
    if arr[i] == '('
    {
      sum +=1;
    } else
    {
      sum -=1;
    }
  }

  fmt.printf("P1 res: %d", sum)
}

P2 :: proc()
{
  path: string;
  if (sample)
  {
    path = path_sample;
  } else
  {
    path = path_main;
  }
  arr: []byte;
  arr , _ = os.read_entire_file(path, context.allocator);
  defer delete(arr);
  sum := 0;
  i := 0;
  for ;i < len(arr); i+=1
  {
    if arr[i] == '('
    {
      sum +=1;
    } else
    {
      sum -=1;
    }
    if sum == -1
    {
      break;
    }
  }

  fmt.printf("\nP2 res: %d", i  +1)
}