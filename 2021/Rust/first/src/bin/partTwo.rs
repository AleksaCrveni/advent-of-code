use std::collections::HashMap;
use std::fs::File;
use std::io::{BufRead, BufReader};
// cargo run --bin partTwo
fn main() {
    let sample = false;
    // NOTE: have to run this from /src folder since path is calculated from where cargon run cmd
    // is run and not where the main.rs file is

    // TODO see if this can be more performant
    let mut path = "input2.txt";
    if sample {
        path = "inputSimple2.txt";
    }

    let file = match File::open(path) {
        Ok(f) => f,
        Err(_err) => panic!("Error reading file: {:?}", _err.to_string())
    };

    let reader = BufReader::new(file);
    let mut counter:i32 = 0;
    let mut sum = 0;
    let mut i = 0;
    let mut last_three: [i32; 3] = [0; 3];
    for line in reader.lines() {
        let val: i32 = match line {
            Ok(l) => l.parse().unwrap(),
            Err(_err) => panic!("Split panic! Err: {:?}", _err)
        };
        if i < 3 {
            sum += val;
            last_three[i] = val;
        } else {
            let prev = sum;

            sum -= last_three[0];
            sum += val;
            if prev < sum {
                counter += 1;
            }
            // shift left arr
            last_three[0] = last_three[1];
            last_three[1] = last_three[2];
            last_three[2] =  val;
        }

        i += 1
    }
    println!("RESULT: {}", counter);
}


