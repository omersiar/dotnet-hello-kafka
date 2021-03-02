// Copyright 2020 Confluent Inc.

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Confluent.Kafka;
using System;


class Program
    {
        static void Main(string[] args)
        {
            var pConfig = new ProducerConfig
            {
                BootstrapServers = "kafka.confluent.svc.cluster.local:9092",
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                // Note: If your root CA certificates are in an unusual location you
                // may need to specify this using the SslCaLocation property.
                SaslUsername = "test",
                SaslPassword = "test123"
            };
            
            using (var producer = new ProducerBuilder<Null, string>(pConfig).Build())
            {
                producer.Produce("demo", new Message<Null, string> { Value = "World Dominance" });
                // block until all in-flight produce requests have completed (successfully
                // or otherwise) or 10s has elapsed.
                producer.Flush(TimeSpan.FromSeconds(10));
            }
            
        }
    }