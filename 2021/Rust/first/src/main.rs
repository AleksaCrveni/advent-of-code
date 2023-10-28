use std::fs::File;
use std::io::{BufRead, BufReader};

fn main() {
    let sample = true;
    // NOTE: have to run this from /src folder since path is calculated from where cargon run cmd
    // is run and not where the main.rs file is
    let mut path = "input.txt";
    if sample {
        path = "inputSimple.txt";
    }

    let file = match File::open("inputSimple.txt") {
        Ok(f) => f,
        Err(_err) => panic!("Error reading file: {:?}", _err.to_string())
    };

    let reader = BufReader::new(file);

    for line in reader.lines() {
        println!("{:?}", line);
    }

}
