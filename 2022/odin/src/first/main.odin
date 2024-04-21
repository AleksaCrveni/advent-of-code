package day_1

import "core:fmt"
import "core:os"
import "core:strings"
import "core:strconv"
import util "../shared"

main :: proc() { 
  sample: bool = false
  filename := "input.txt"
  if sample {
    filename = "input_sample.txt"
  }

  // allocating on heap, but we could allocate on stack if we declare variable here and pass it to be filled in
  data, ok := util.read_file_original(filename)
  if !ok {
    panic("err reading file")
  }

  first_part(data)
  second_part(data)
}

first_part :: proc(byte_arr: []byte) {
  data := string(byte_arr)
  max_cal := -1
  current_cal := 0
  for line in strings.split_lines_iterator(&data) {
    if line != "" {
      current_cal += strconv.atoi(line)
    } else {
      if current_cal > max_cal {
        max_cal = current_cal
      }
      current_cal = 0
    }
  }

  fmt.printf("Part One Result: %v", max_cal)
}

second_part :: proc(byte_arr: []byte) {
  data := string(byte_arr)
  top3: [3]int = {0,0,0}
  current_cal := 0
  for line in strings.split_lines_iterator(&data) {
    if line != ""  {
      current_cal += strconv.atoi(line) 
      continue
    }
    // check if current is in top 3
    check_top_and_shift(&top3, &current_cal)
  }
  check_top_and_shift(&top3, &current_cal)
    
  sum := 0
  for i in 0..< len(top3) {
    sum += top3[i]
  }

  fmt.printf("\nPart 2 Result: %v", sum)
}

check_top_and_shift :: proc(top3: ^[3]int, current_cal: ^int) {
  if top3[0] < current_cal^ {
    ind := 0
    for i in 1..< 3 {
      // find place
      if top3[i] < current_cal^ {
        //fmt.printf("\n%v bigger than %v", current_cal, top3[i])
        ind = i
      }
    }

    for i in 0..< ind {
      top3[i] = top3[i+1]
    }
    top3[ind] = current_cal^
    
  }
  current_cal^ = 0
}
