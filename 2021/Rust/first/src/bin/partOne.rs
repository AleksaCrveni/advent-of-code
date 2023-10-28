use std::fs::File;
use std::io::{BufRead, BufReader};
// cargo run --bin partOne
fn main() {
    let sample = false;
    // NOTE: have to run this from /src folder since path is calculated from where cargon run cmd
    // is run and not where the main.rs file is
    let mut path = "input1.txt";
    if sample {
        path = "inputSimple11.txt";
    }

    let file = match File::open(path) {
        Ok(f) => f,
        Err(_err) => panic!("Error reading file: {:?}", _err.to_string())
    };

    let reader = BufReader::new(file);
    let mut prev_value = i32::MAX;
    let mut counter:i32 = 0;
    for line in reader.lines() {
        let val:i32 = match line {
            Ok(l) => l.parse().unwrap(),
            Err(_err) => continue
        };
        if val > prev_value {
            counter += 1;
        }
        prev_value = val;
    }
    println!("RESULT: {}", counter);
}
