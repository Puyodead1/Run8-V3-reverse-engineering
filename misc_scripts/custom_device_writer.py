import os
import select
import signal
import socket
from enum import Enum

os.environ["PYGAME_HIDE_SUPPORT_PROMPT"] = "hide"
import pygame

PORT = 18888


class UDP_IO_MessageType(Enum):
    INDY_BRAKE_LEVER = 9


def int_to_byte(number):
    return number & 0xFF


def encode_double(value):
    value = abs(value)
    if value < 10.0:
        return int_to_byte(round(value * 10.0) | 128)
    if value <= 127.0:
        return int_to_byte(round(value))
    return 127


class UDPServer:
    def __init__(self):
        self.running = True
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        self.target_address = ("127.0.0.1", PORT)
        print(f"Server targeting port {PORT}")

        signal.signal(signal.SIGINT, self.handle_shutdown)
        signal.signal(signal.SIGTERM, self.handle_shutdown)

    def handle_shutdown(self, signum, frame):
        """Handle shutdown signal gracefully"""
        print("\nShutdown signal received...")
        self.running = False

    def calculate_crc(self, data: bytes):
        b = data[0]
        for i in range(1, len(data)):
            b ^= data[i]
        return b

    def build_packet(self, msg_type: UDP_IO_MessageType, value: float):
        packet = bytearray([96])

        # add the message type where the first byte is the high byte and the second byte is the low byte
        packet.append(msg_type.value >> 8)
        packet.append(msg_type.value & 0xFF)

        # value
        packet.append(value)

        # Calculate and append CRC
        crc = self.calculate_crc(packet)
        packet.append(crc)

        return packet

    def send_data(self, msg_type: UDP_IO_MessageType, value: float):
        try:
            final_data = self.build_packet(msg_type, value)
            self.socket.sendto(final_data, self.target_address)
            print(f"Sent message: {len(final_data)} bytes")
            print(f"Raw bytes: {[hex(b) for b in final_data]}")
        except Exception as e:
            print(f"Failed to send data: {e}")

    def close(self):
        self.socket.close()


def map_float_to_byte(value):
    # First clamp value to [-1, 1] range
    value = max(-1.0, min(1.0, value))

    # Map [-1, 1] to [0, 255]
    # First map to [0, 1]
    normalized = (value + 1) / 2
    # Then map to [0, 255]
    byte_value = int(normalized * 255)

    return byte_value


def main():
    pygame.display.init()
    pygame.joystick.init()
    if pygame.joystick.get_count() == 0:
        print("No joysticks found.")
        return

    # print the found joysticks
    for i in range(pygame.joystick.get_count()):
        print(f"Joystick {i}: {pygame.joystick.Joystick(i).get_name()}")

    joystick = pygame.joystick.Joystick(0)
    joystick.init()

    server = UDPServer()

    try:
        print("Server running... (Press Ctrl+C to exit)")
        while server.running:
            pygame.event.pump()
            value = joystick.get_axis(3)
            value = map_float_to_byte(-value)
            # value = encode_double(-value)
            print(f"Value: {value}")
            server.send_data(UDP_IO_MessageType.INDY_BRAKE_LEVER, value)
    except KeyboardInterrupt:
        print("\nKeyboard interrupt received...")
    finally:
        server.close()
        print("Server stopped.")


if __name__ == "__main__":
    main()
