import math
import select
import signal
import socket

PORT = 18889


def int_to_byte(number):
    return number & 0xFF


class UDPClient:
    def __init__(self):
        self.running = True
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        self.socket.bind(("127.0.0.1", PORT))
        print(f"Listening on port {PORT}")

        signal.signal(signal.SIGINT, self.handle_shutdown)
        signal.signal(signal.SIGTERM, self.handle_shutdown)

    def handle_shutdown(self, signum, frame):
        """Handle shutdown signal gracefully"""
        print("\nShutdown signal received...")
        self.running = False

    def has_decimal_precision(self, control_byte):
        """Check if decimal precision flag (bit 7) is set"""
        return (control_byte & 128) == 128

    def parse_message_type(self, high_byte, low_byte):
        """Convert two bytes into 16-bit message type"""
        return (high_byte << 8) + low_byte

    def calculate_crc(self, data: bytes):
        b = data[0]
        for i in range(1, len(data)):
            b ^= data[i]

        return b

    def validate_crc(self, data: bytes):
        crc = data[-1]
        return crc == self.calculate_crc(data[:-1])

    def encode_double(self, value):
        value = math.abs(value)
        if value > 127:
            return int_to_byte(round(value * 10.0, 0) | 128)
        if value <= 127.0:
            return int_to_byte(round(value, 0))

        return 127

    def decode_double(self, value):
        if (value & 128) == 128:
            return (value & 127) / 10.0
        return value

    def read_message(self):
        """Read and parse a message from the UDP socket"""
        data, addr = self.socket.recvfrom(1024)

        if len(data) < 4:
            raise ValueError("Message too short")

        incoming_bit = (data[0] & 97) == 97
        if not incoming_bit:
            raise ValueError("Incoming bit not set")

        if not self.validate_crc(data):
            raise ValueError("CRC mismatch")

        """
        0 - message type byte
        1 - control byte or speed?
        2 - MR
        3 - ER?
        4 - BP 1
        5 - BP 2
        6 - BC
        7 - CFM
        8 - Notch
        9 - 
        10 -
        11 - EOT ID
        12 - EOT ID
        13 - EOT ID
        14 - EOT ID
        15 - 
        16 - something to do with traction motor amps
        17 - a signal instruction
        18 - a signal instruction
        19 - 
        20 - reverser (0 - rev, 128 - neutral, 255 - fwd)
        21 - water temp f
        22 - 
        23 - 
        24 -
        """

        # skip bytes 0 and 1 and read the rest
        speed = data[1]
        mr = data[2]
        byte_3 = data[3]  # either ER or BP
        br1 = data[4]
        br2 = data[5]
        bc = data[6]
        cfm = data[7]
        notch = data[8]
        byte_9 = data[9]
        byte_10 = data[10]
        eot_id = data[11:15]
        traction_motor_amps = data[16]
        signal_instruction_1 = data[17]
        signal_instruction_2 = data[18]
        byte_19 = data[19]
        reverser = data[20]
        water_temp_f = data[21]
        byte_22 = data[22]
        byte_23 = data[23]
        byte_24 = data[24]

        reverser_state = "FWD"
        if reverser == 0:
            reverser_state = "REV"
        elif reverser == 127:
            reverser_state = "NEUTRAL"

        print(f"Speed: {self.decode_double(speed)}")
        print(f"MR: {mr}")
        print(f"ER?: {byte_3}")
        print(f"BR 1: {br1}")
        print(f"BR 2: {br2}")
        print(f"BC: {bc}")
        print(f"CFM: {cfm}")
        print(f"Notch: {notch}")
        print(f"Byte 9: {byte_9}")
        print(f"Byte 10: {byte_10}")
        print(f"EOT ID: {eot_id}")
        print(f"Traction Motor Amps: {traction_motor_amps}")
        print(f"Signal Instruction 1: {signal_instruction_1}")
        print(f"Signal Instruction 2: {signal_instruction_2}")
        print(f"Byte 19: {byte_19}")
        print(f"Reverser: {reverser_state}")
        print(f"Water Temp F: {water_temp_f}")
        print(f"Byte 22: {byte_22}")
        print(f"Byte 23: {byte_23}")
        print(f"Byte 24: {byte_24}")
        print()

    def close(self):
        self.socket.close()


def main():
    client = UDPClient()
    try:
        print("Waiting for messages... (Press Ctrl+C to exit)")
        while client.running:
            ready, _, _ = select.select([client.socket], [], [], 1)
            if ready:
                try:
                    client.read_message()
                except ValueError as e:
                    print(f"Error parsing message: {e}")
                except Exception as e:
                    print(f"Unexpected error: {e}")
    except KeyboardInterrupt:
        print("\nKeyboard interrupt received...")
    finally:
        client.close()
        print("Client stopped.")


if __name__ == "__main__":
    main()
