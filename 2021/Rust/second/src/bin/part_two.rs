use std::fs::File;
use std::io::{BufRead, BufReader};

fn main() {
    let simple = false;

    let mut path = "input2.txt";
    if simple {
        path = "inputSimple2.txt";
    }

    let file = match File::open(path) {
        Ok(f) => f,
        Err(_err) => panic!("Err opening file. Err: {}", _err.to_string())
    };

    let reader = BufReader::new(file);
    let mut h_pos: i32 = 0;
    let mut depth: i32 = 0;
    let mut aim: i32 = 0;

    for line in  reader.lines() {
        if let split_lines = line.unwrap().split(' ').collect::<Vec<&str>>() {
            _ = match split_lines[0] {
                "forward" => {
                    let val = split_lines[1].parse::<i32>().unwrap();
                    h_pos += val;
                    depth += aim * val;
                },
                "down" => {
                    aim += split_lines[1].parse::<i32>().unwrap();
                },
                "up" => {
                    aim -= split_lines[1].parse::<i32>().unwrap();
                },
                _ => continue
            }
        }

    }
    println!("RESULT: {}", depth * h_pos);
}
