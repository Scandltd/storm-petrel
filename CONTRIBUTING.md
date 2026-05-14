# Contributing to Scand Storm Petrel

Thank you for your interest in **Scand Storm Petrel**! We appreciate the community's efforts to help improve this product.

To maintain a high standard of quality and ensure legal clarity for both contributors and our company, we ask that you follow these guidelines.

## 1. Contributor License Agreement (CLA)

By submitting a Pull Request (PR) to this repository, you agree to the following:
* **Grant of License.** You grant Scand a perpetual, worldwide, non-exclusive, no-charge, royalty-free, irrevocable copyright license to reproduce, prepare derivative works of, publicly display, publicly perform, sublicense, and distribute your contributions and such derivative works.
* **Right to Contribute.** You represent that you are the legally entitled to grant the above license. If your employer(s) has rights to intellectual property that you create, you represent that you have received permission to make contributions on behalf of that employer.
* **Outbound License.** You agree that your contributions will be licensed under the project's primary license (MIT License) or any other license that Scand deems appropriate for the project in the future.
* **No Warranty.** You provide your contributions on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

## 2. How to Contribute

We follow the standard **Fork & Pull** workflow:

1. **Report** and **discuss** your issue first.
2. **Fork** `main` branch of the repository to your own GitHub account.
3. **Create a branch** for your feature or fix (`git checkout -b dev-<feature-related-issue-number>` or `bugfix-<bug-related-issue-number>`).
4. **Commit your changes** with clear, descriptive commit messages.
5. **Push** to your fork and **open a Pull Request** targeting our `main` branch.

## 3. Code Review and Merging

All contributions are subject to a formal review process:

* **CI/CD Checks:** Ensure that all automated tests pass before requesting a review. See [build.ps1](build/build.ps1) for more details.
* **Code Owners Review:** Members of our core development team will review your code. We may suggest improvements or request changes to align with our internal architecture.
* **Exclusive Merge Rights:** Final approval and the right to merge into the `main` branch are held exclusively by our company’s core team.

## 4. Coding Standards

To streamline the review process:
* Follow the coding style and conventions used throughout the project, see `.editorconfig` files in child directories for more details.
* Include unit tests for any new features or bug fixes whenever possible.
* Ensure your code is well-documented and follows professional best practices.

## 5. Reporting Issues

If you encounter a bug or have a feature request:
1. Check the [Issues](https://github.com/Scandltd/storm-petrel/issues) tab to see if it has already been reported.
2. If not, open a new issue with a detailed description, including steps to reproduce the bug and your environment details.

---
*Thank you for helping us make Scand Storm Petrel better!*
