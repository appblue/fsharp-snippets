namespace Avg 

type IStat<'T, 'U> =
    abstract Record: 'T -> unit
    abstract Value: 'U
    abstract Count: int

[<AutoOpen>]
module Factory =

    let makeAverager(toFloat: 'T -> float) =

        let mutable count = 0
        let mutable total = 0.0

        {
            new IStat<'T, float> with
            
                member _.Record(x) =
                    count <- count + 1
                    total <- total + toFloat x

                member _.Value =
                    total / float count

                member _.Count =
                    count
        }