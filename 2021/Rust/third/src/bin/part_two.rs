use std::collections::HashMap;
use std::fs::File;
use std::io::{BufRead, BufReader};
use std::collections::HashSet;
use std::ops::Index;

fn main() {
    let simple = true;
    let mut path = "input.txt";

    if simple {
        path = "inputSimple.txt";
    }

    let file = match File::open(path) {
        Ok(f) => f,
        Err(_err) => panic!("Error during opening file: Err: {}", _err.to_string())
    };

    let reader = BufReader::new(file);
    let mut zero_array: Vec<i32> = Vec::new();
    let mut one_array: Vec<i32> = Vec::new();
    let mut index: Vec<String> = Vec::new();
    for (i, line) in reader.lines().enumerate() {
        index.as_mut().push(line.unwrap());
        if let chars = line.unwrap().chars() {
            if i == 0 {
                let len = chars.clone().count();
                for j in 0..len {
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
    let mut common_array:Vec<char> = Vec::new();
    for i in 0..one_array.len() {
        if one_array[i] > zero_array[i] {
            common_array.push('1')
        } else {
            common_array.push('0');
        }
    }

    // oxygen generator rating
    // instead of removing we could probably mark it as -1 or something and jus iterate over
    // I think when it removes from vector it has to shift array
    let mut ogr: &str = "";
    for i in 0..common_array.len() {
        let bit = common_array[0];

        for ( i, l ) in index.iter().enumerate() {
            if bit == l.clone().chars().collect::<Vec<char>>()[0] {

            }
        }
        if index.len() == 1 {
           ogr = index[0].as_str()
        }
    }
    /*
    let gamma_val = isize::from_str_radix(gamma.as_str(), 2).unwrap();
    let epsilon_val = isize::from_str_radix(epsilon.as_str(), 2).unwrap();
     */
    println!("{}", ogr)

}
