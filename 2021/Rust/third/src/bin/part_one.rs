use std::collections::HashMap;
use std::fs::File;
use std::io::{BufRead, BufReader};


fn main() {
    let simple = false;
    let mut path = "input1.txt";

    if simple {
        path = "input1Simple.txt";
    }

    let file = match File::open(path) {
        Ok(f) => f,
        Err(_err) => panic!("Error during opening file: Err: {}", _err.to_string())
    };

    let reader = BufReader::new(file);
    let mut zero_array: Vec<i32> = Vec::new();
    let mut one_array: Vec<i32> = Vec::new();
    let mut bin_size: usize = 0;
    for (i, line) in reader.lines().enumerate() {
        if let chars = line.unwrap().chars() {
            if i == 0 {
                bin_size = chars.clone().count();
                for _ in 0..bin_size {
                    zero_array.push(0);
                    one_array.push(0);
                }
            }
            for (i, c) in chars.enumerate() {
                if c == '0' {
                    zero_array[i] += 1;
                    continue;
                }
                one_array[i] += 1;
            }
        }
    }
    let mut gamma: String = "".to_string();
    let mut epsilon: String = "".to_string();
    for i in 0..bin_size {
        if one_array[i] > zero_array[i] {
            gamma.push_str("1");
            epsilon.push_str("0")
        } else {
            gamma.push_str("0");
            epsilon.push_str("1")
        }
    }
    let gamma_val = isize::from_str_radix(gamma.as_str(), 2).unwrap();
    let epsilon_val = isize::from_str_radix(epsilon.as_str(), 2).unwrap();

    println!("RESULT: {}", gamma_val * epsilon_val);

}
