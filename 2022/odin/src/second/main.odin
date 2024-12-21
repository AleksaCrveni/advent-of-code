package day_2

import "core:strings"
import "core:mem"
import "core:fmt"
import "core:unicode/utf8"
import "core:strconv"
import utils "../../shared"
main :: proc() {
  sample: bool = false
  filename := "input.txt"
  if sample {
    filename = "input_sample.txt"
  }

  data, ok := utils.read_file_original(filename)
  defer delete(data)
  if !ok {
    panic("err reading file")
  }

  first_part(data)
  second_part(data)
}

/*
  Elf:
    Rock -> A
    Paper -> B
    Scissors -> C
  ----------------------
  Me:
    Rock -> X
    Paper -> Y
    Scissors -> Z
  -----------------
  
  Rock -> 1p
  Paper -> 2p
  Scissors -> 3p

  Win -> 6p
  Draw -> 3p
  Lose -> 0p

  Rock defeats Scissors -> X > C
  Scissors defeats Paper -> Z > B
  Paper defeats Rock -> Y > A
  If both players choose the same shape, the round instead ends in a draw.

*/
first_part :: proc(byte_arr: []byte) {
  track: mem.Tracking_Allocator
  mem.tracking_allocator_init(&track, context.allocator)
  defer {
    fmt.printf("=== %v items leaked ===", len(track.allocation_map))
    for _, entry in track.allocation_map {
      fmt.printf("- %v (%vB)", entry.location, entry.size)
    }
  }
  context.allocator = mem.tracking_allocator(&track)

  data := string(byte_arr)
  my_map := map[rune]int{'X' = 1, 'Y' = 2, 'Z' = 3}
  elf_map := map[rune]int{'A' = 1, 'B' = 2, 'C' = 3}
  defer delete(my_map)
  defer delete(elf_map)

  // results map
  results_map := map[int]int{-1 = 0, 2 = 0, 0 = 3, -2 = 6, 1 = 6}
  defer delete(results_map)
  points := 0
  for line in strings.split_lines_iterator(&data) {
    // index 0 is elf and index 2 is me
    runes := utf8.string_to_runes(line, context.allocator)
    defer delete(runes)
    //dif := strconv.atoi(elf) - strconv.atoi(me)
    points = points + my_map[runes[2]]

    /*
    This can be obviously done with simple if else statements,
    but i wanted to see if there is some logic connection to the results
    1 -1 = 0 - draw 
    1 - 2 = -1 - loss
    1 - 3 = -2 - win 
    
    2 - 1 = 1 - win
    2 - 2 = 0 - draw
    2 - 3 - -1 - loss

    3 - 1 = 2 - loss
    3 - 2 = 1 - win 
    3 - 3 = 0 - draw

    loss -> -1, -1, 2
    win -> -2, 1, 1
    draw -> 0
    */

    diff := my_map[runes[2]] - elf_map[runes[0]]
    points = points + results_map[int(diff)]
  }

  fmt.printf("P1 Results: %d\n", points)
}

second_part :: proc(byte_arr: []byte) {

}


